using EventBus.Base;
using EventBus.Base.Abstraction;
using EventBus.Factory;
using EventBus.UnitTest.Events.EventHandlers;
using EventBus.UnitTest.Events.Events;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RabbitMQ.Client;

namespace EventBus.UnitTest
{
    [TestClass]
    public class EventBusTests
    {
        private IEventBus eventBus;

        public EventBusTests()
        {
            ServiceCollection services = new();
            services.AddSingleton(sp =>
            {
                return EventBusFactory.Create(new()
                {
                    ConnectionRetryCount = 5,
                    SubscriberClientAppName = "EvetBus.UnitTest",
                    DefaultTopicName = "SellingBuddyTopicName",
                    EventBusType = EventBusType.RabbitMQ,
                    EventNameSuffix = "IntegrationEvent",
                    Connection = new ConnectionFactory()
                    {
                        HostName = "207.154.222.131",
                        Port = 5672,
                        UserName = "guest",
                        Password = "guest"
                    }
                }, sp)!;
            });

            eventBus = services.BuildServiceProvider().GetRequiredService<IEventBus>();
        }

        [TestMethod]
        public void SubscribeEventOnRabbitmqTest()
        {
            eventBus.Subscribe<OrderCreatedIntegrationEvent, OrderCreatedIntegrationEventHandler>();
            //eventBus.UnSubscribe<OrderCreatedIntegrationEvent, OrderCreatedIntegrationEventHandler>();
        }

        [TestMethod]
        public void SendMessageToRabbitMQ()
        {
            eventBus.Publish(new OrderCreatedIntegrationEvent(1));
        }
    }
}
