using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SqlPersistence.Configurations
{
    public abstract class EntityBaseConfiguration<T> : IEntityTypeConfiguration<T> where T : class, IEntityBaseInt
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.HasKey(e => e.Id);
        }
    }
}
