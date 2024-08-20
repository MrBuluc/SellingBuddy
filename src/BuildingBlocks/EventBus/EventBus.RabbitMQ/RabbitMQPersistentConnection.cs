using Polly;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;
using System.Net.Sockets;

namespace EventBus.RabbitMQ
{
    public class RabbitMQPersistentConnection(IConnectionFactory connectionFactory, int retryCount = 5) : IDisposable
    {
        private readonly IConnectionFactory connectionFactory = connectionFactory;
        private readonly int retryCount = retryCount;
        private IConnection connection;
        private object lock_object = new();
        private bool _disposed;

        public bool IsConnected => connection is not null && connection.IsOpen;

        public IModel CreateModel() => connection.CreateModel();

        public void Dispose()
        {
            _disposed = true;
            connection?.Dispose();
        }

        public bool TryConnect()
        {
            lock (lock_object)
            {
                Policy.Handle<SocketException>()
                    .Or<BrokerUnreachableException>()
                    .WaitAndRetry(retryCount, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), (ex, time) => { })
                    .Execute(() =>
                    {
                        connection = connectionFactory.CreateConnection();
                    });

                if (IsConnected && connection is not null)
                {
                    connection.ConnectionShutdown += Connection_ConnectionShutdown;
                    connection.CallbackException += Connection_CallbackException;
                    connection.ConnectionBlocked += Connection_ConnectionBlocked;

                    //log

                    return true;
                }

                return false;
            }
        }

        private void Connection_ConnectionBlocked(object? sender, ConnectionBlockedEventArgs e)
        {
            if (!_disposed)
            {
                TryConnect();
            }
        }

        private void Connection_CallbackException(object? sender, CallbackExceptionEventArgs e)
        {
            if (!_disposed)
            {
                TryConnect();
            }
        }

        private void Connection_ConnectionShutdown(object? sender, ShutdownEventArgs e)
        {
            // log Connection_ConnectionShutdown
            if (!_disposed)
            {
                TryConnect();
            }
        }
    }
}
