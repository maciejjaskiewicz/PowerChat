using System;

namespace PowerChat.Domain.Common
{
    public interface IEntity
    {
        long Id { get; set; }
    }

    public abstract class Entity : IEntity
    {
        public long Id { get; set; }
    }

    public interface IAuditableEntity : IEntity
    {
        DateTime CreatedDate { get; set; }
        DateTime? ModifiedDate { get; set; }
    }

    public abstract class AuditableEntity : Entity, IAuditableEntity
    {
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }

    public interface IDeletableEntity : IEntity
    {
        bool IsDeleted { get; set; }
    }
}
