using EventBus.Base;
using EventBus.Base.Events;
using Newtonsoft.Json;
using Polly;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;
using System.Net.Sockets;
using System.Text;

namespace EventBus.RabbitMQ
{
    public class EventBusRabbitMQ : BaseEventBus
    {
        RabbitMQPersistentConnection persistentConnection;
        private readonly IConnectionFactory connectionFactory;
        private readonly IModel consumerChannel;
        private readonly EventBusConfig config;

        public EventBusRabbitMQ(EventBusConfig config, IServiceProvider serviceProvider) : base(serviceProvider, config)
        {
            if (config.Connection is not null)
            {
                if (config.Connection is ConnectionFactory)
                {
                    connectionFactory = (ConnectionFactory)config.Connection;
                }
                else
                {
                    connectionFactory = JsonConvert.DeserializeObject<ConnectionFactory>(JsonConvert.SerializeObject(config.Connection, new JsonSerializerSettings()
                    {
                        // Self referencing loop detected for prop
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    }));
                }
            }
            else
            {
                connectionFactory = new ConnectionFactory();
            }

            persistentConnection = new RabbitMQPersistentConnection(connectionFactory, config.ConnectionRetryCount);

            this.config = config;
            consumerChannel = CreateConsumerChannel(this.config);

            SubsManager.OnEventRemoved += SubsManager_OnEventRemoved;
        }

        private void SubsManager_OnEventRemoved(object? sender, string eventName)
        {
            eventName = ProcessEventName(eventName);

            if (!persistentConnection.IsConnected)
            {
                persistentConnection.TryConnect();
            }

            consumerChannel.QueueUnbind(queue: eventName, exchange: config.DefaultTopicName, routingKey: eventName);

            if (SubsManager.IsEmpty)
            {
                consumerChannel.Close();
            }
        }

        public override void Publish(IntegrationEvent @event)
        {
            if (!persistentConnection.IsConnected)
            {
                persistentConnection.TryConnect();
            }

            string eventName = ProcessEventName(@event.GetType().Name);

            consumerChannel.ExchangeDeclare(exchange: config.DefaultTopicName, type: "direct"); // Ensure exchange exists while publishing

            Policy.Handle<BrokerUnreachableException>()
                .Or<SocketException>()
                .WaitAndRetry(config.ConnectionRetryCount, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), (ex, time) =>
                {
                    // log
                }).Execute(() =>
            {
                IBasicProperties properties = consumerChannel.CreateBasicProperties();
                properties.DeliveryMode = 2; // persistent

                consumerChannel.QueueDeclare(queue: GetSubName(eventName), // Ensure queue exists while publishing
                                                                           durable: true, exclusive: false, autoDelete: false, arguments: null);
                consumerChannel.QueueBind(queue: GetSubName(eventName), exchange: config.DefaultTopicName, routingKey: eventName);

                consumerChannel.BasicPublish(exchange: config.DefaultTopicName, routingKey: eventName, mandatory: true, basicProperties: properties, body: Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(@event)));
            });

        }

        public override void Subscribe<T, TH>()
        {
            string eventName = typeof(T).Name;
            eventName = ProcessEventName(eventName);

            if (!SubsManager.HasSubscriptionsForEvent(eventName))
            {
                if (!persistentConnection.IsConnected)
                {
                    persistentConnection.TryConnect();
                }

                consumerChannel.QueueDeclare(queue: GetSubName(eventName), // Ensure queue exists while consuming
                                                                           durable: true, exclusive: false, autoDelete: false,
                                                                           arguments: null);
                consumerChannel.QueueBind(queue: GetSubName(eventName), exchange: config.DefaultTopicName, routingKey: eventName);
            }

            SubsManager.AddSubscription<T, TH>();
            StartBasicConsumer(eventName);
        }

        public override void UnSubscribe<T, TH>()
        {
            SubsManager.RemoveSubscription<T, TH>();
        }

        private IModel CreateConsumerChannel(EventBusConfig config)
        {
            if (!persistentConnection.IsConnected)
            {
                persistentConnection.TryConnect();
            }

            IModel channel = persistentConnection.CreateModel();
            channel.ExchangeDeclare(exchange: config.DefaultTopicName, type: "direct");

            return channel;
        }

        private void StartBasicConsumer(string eventName)
        {
            if (consumerChannel is not null)
            {
                EventingBasicConsumer consumer = new(consumerChannel);
                consumer.Received += Consumer_Received;

                consumerChannel.BasicConsume(queue: GetSubName(eventName), autoAck: false, consumer: consumer);
            }
        }

        private async void Consumer_Received(object? sender, BasicDeliverEventArgs eventArgs)
        {
            string eventName = eventArgs.RoutingKey;
            eventName = ProcessEventName(eventName);

            try
            {
                await ProcessEvent(eventName, Encoding.UTF8.GetString(eventArgs.Body.Span));
            }
            catch (Exception ex)
            {
                // logging
            }

            consumerChannel.BasicAck(eventArgs.DeliveryTag, multiple: false);
        }
    }
}
