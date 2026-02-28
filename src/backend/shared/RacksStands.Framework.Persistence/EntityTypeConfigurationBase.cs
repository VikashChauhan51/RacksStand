using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace RacksStands.Framework.Persistence;

public abstract class EntityTypeConfigurationBase<TEntity, TKey> : IEntityTypeConfiguration<TEntity>
    where TEntity : EntityBase<TKey>
{
    public abstract void Configure(EntityTypeBuilder<TEntity> builder);
}
