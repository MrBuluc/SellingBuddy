﻿using EventBus.Base.Abstraction;
using EventBus.Base.Events;

namespace EventBus.Base.SubManagers
{
    public class InMemoryEventBusSubscriptionManager : IEventBusSubscriptionManager
    {
        private readonly Dictionary<string, List<SubscriptionInfo>> _handlers;
        private readonly List<Type> _eventTypes;

        public event EventHandler<string> OnEventRemoved;
        public Func<string, string> eventNameGetter;
        public bool IsEmpty => _handlers.Keys.Count == 0;

        public InMemoryEventBusSubscriptionManager(Func<string, string> eventNameGetter)
        {
            _handlers = [];
            _eventTypes = [];
            this.eventNameGetter = eventNameGetter;
        }

        public void AddSubscription<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>
        {
            AddSubscription(typeof(TH), GetEventKey<T>());

            if (!_eventTypes.Contains(typeof(T)))
            {
                _eventTypes.Add(typeof(T));
            }
        }

        private void AddSubscription(Type handlerType, string eventName)
        {
            if (!HasSubscriptionsForEvent(eventName))
            {
                _handlers.Add(eventName, []);
            }

            if (_handlers[eventName].Any(s => s.HandlerType == handlerType))
            {
                throw new ArgumentException($"Handler Type {handlerType.Name} already registered for '{eventName}'", nameof(handlerType));
            }

            _handlers[eventName].Add(SubscriptionInfo.Typed(handlerType));
        }

        public void Clear() => _handlers.Clear();

        public string GetEventKey<T>() => eventNameGetter(typeof(T).Name);

        public Type? GetEventTypeByName(string eventName) => _eventTypes.SingleOrDefault(t => t.Name == eventName);

        public IEnumerable<SubscriptionInfo> GetHandlersForEvent<T>() where T : IntegrationEvent => GetHandlersForEvent(GetEventKey<T>());

        public IEnumerable<SubscriptionInfo> GetHandlersForEvent(string eventName) => _handlers[eventName];

        public bool HasSubscriptionsForEvent<T>() where T : IntegrationEvent => HasSubscriptionsForEvent(GetEventKey<T>());

        public bool HasSubscriptionsForEvent(string eventName) => _handlers.ContainsKey(eventName);

        public void RemoveSubscription<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>
        {

            SubscriptionInfo? subsToRemove = FindSubscriptionToRemove<T, TH>();
            if (subsToRemove is not null)
            {
                RemoveHandler(GetEventKey<T>(), subsToRemove);
            }
        }

        private SubscriptionInfo? FindSubscriptionToRemove<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T> => FindSubscriptionToRemove(GetEventKey<T>(), typeof(TH));

        private SubscriptionInfo? FindSubscriptionToRemove(string eventName, Type handlerType)
        {
            if (!HasSubscriptionsForEvent(eventName))
            {
                return null;
            }

            return _handlers[eventName].SingleOrDefault(s => s.HandlerType == handlerType);
        }

        private void RemoveHandler(string eventName, SubscriptionInfo subsToRemove)
        {
            if (subsToRemove is not null)
            {
                _handlers[eventName].Remove(subsToRemove);

                if (_handlers[eventName].Count == 0)
                {
                    _handlers.Remove(eventName);
                    Type? eventType = _eventTypes.SingleOrDefault(e => e.Name == eventName);
                    if (eventType is not null)
                    {
                        _eventTypes.Remove(eventType);
                    }

                    RaiseOnEventRemoved(eventName);
                }
            }
        }

        private void RaiseOnEventRemoved(string eventName)
        {
            OnEventRemoved?.Invoke(this, eventName);
        }
    }
}
