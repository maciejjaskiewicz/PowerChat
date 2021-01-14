using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PowerChat.Common.Domain;

namespace PowerChat.Services.Users.Infrastructure.Persistence.Configurations
{
    public abstract class EntityConfigurationBase<TEntity> : IEntityTypeConfiguration<TEntity>
        where TEntity : class, IEntity
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.ToTable(typeof(TEntity).Name);
            builder.HasKey(x => x.Id);
        }

        protected EnumToStringConverter<TEnum> EnumConverter<TEnum>() where TEnum : struct
        {
            return new EnumToStringConverter<TEnum>();
        }
    }
}
