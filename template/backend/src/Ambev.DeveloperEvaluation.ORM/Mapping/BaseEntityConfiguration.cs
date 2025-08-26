using Ambev.DeveloperEvaluation.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping;

/// <summary>
/// Base configuration for entities that inherit from BaseEntity
/// </summary>
/// <typeparam name="T">Entity type that inherits from BaseEntity</typeparam>
public abstract class BaseEntityConfiguration<T> : IEntityTypeConfiguration<T> where T : BaseEntity
{
    /// <summary>
    /// Configures common properties for BaseEntity
    /// </summary>
    /// <param name="builder">Entity type builder</param>
    public virtual void Configure(EntityTypeBuilder<T> builder)
    {
        // Primary Key
        builder.HasKey(x => x.Id);
        builder.Property(s => s.Id).HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");

        // Audit fields
        builder.Property(x => x.CreatedAt)
               .IsRequired()
               .HasColumnType("timestamp with time zone")
               .HasComment("Timestamp when the entity was created");

        builder.Property(x => x.UpdatedAt)
               .IsRequired(false)
               .HasColumnType("timestamp with time zone")
               .HasComment("Timestamp when the entity was last updated");

        // Base indexes for audit fields
        builder.HasIndex(x => x.CreatedAt)
               .HasDatabaseName($"IX_{typeof(T).Name}_CreatedAt")
               .IsUnique(false);

        builder.HasIndex(x => x.UpdatedAt)
               .HasDatabaseName($"IX_{typeof(T).Name}_UpdatedAt")
               .IsUnique(false);
    }
}