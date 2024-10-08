﻿using EventBus.Base;
using EventBus.Base.Abstraction;
using EventBus.RabbitMQ;

namespace EventBus.Factory
{
    public static class EventBusFactory
    {
        public static IEventBus? Create(EventBusConfig config, IServiceProvider serviceProvider) => config.EventBusType switch
        {
            EventBusType.RabbitMQ => new EventBusRabbitMQ(config, serviceProvider),
            _ => null
        };
    }
}
