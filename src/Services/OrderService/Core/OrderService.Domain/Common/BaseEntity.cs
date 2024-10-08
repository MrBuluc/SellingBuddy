﻿using MediatR;

namespace OrderService.Domain.Common
{
    public abstract class BaseEntity : IBaseEntity
    {
        public virtual Guid Id { get; set; }
        private List<INotification>? domainEvents;
        public IReadOnlyCollection<INotification>? DomainEvents => domainEvents?.AsReadOnly();

        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public void AddDomainEvent(INotification notification)
        {
            domainEvents ??= [];
            domainEvents.Add(notification);
        }

        public void ClearDomainEvents()
        {
            domainEvents?.Clear();
        }

        public static bool operator ==(BaseEntity left, BaseEntity right) => Equals(left, null) ? Equals(right, null) : left.Equals(right);

        public override bool Equals(object? obj)
        {
            if (obj == null || obj is not BaseEntity) return false;

            if (ReferenceEquals(this, obj)) return true;

            if (GetType() != obj.GetType()) return false;

            BaseEntity baseEntity = (BaseEntity)obj;

            if (baseEntity.IsTransient() || IsTransient()) return false;

            return baseEntity.Id == Id;
        }

        public bool IsTransient() => Id == default;

        public static bool operator !=(BaseEntity left, BaseEntity right) => !(left == right);
    }
}
