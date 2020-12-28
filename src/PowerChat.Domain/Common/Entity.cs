using System;

namespace PowerChat.Domain.Common
{
    public interface IEntity<TId>
    {
        TId Id { get; set; }
    }

    public abstract class Entity<TId> : IEntity<TId>
    {
        public TId Id { get; set; }
    }
    public abstract class Entity : Entity<long> { }

    public interface IAuditableEntity<TId> : IEntity<TId>
    {
        DateTime CreatedDate { get; set; }
        DateTime? ModifiedDate { get; set; }
    }

    public interface IAuditableEntity : IAuditableEntity<long> { }

    public abstract class AuditableEntity<TId> : Entity<TId>, IAuditableEntity<TId>
    {
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }

    public abstract class AuditableEntity : AuditableEntity<long> { }

    public interface IDeletableEntity<TId> : IEntity<TId>
    {
        bool IsDeleted { get; set; }
    }

    public interface IDeletableEntity : IDeletableEntity<long> { }
}
