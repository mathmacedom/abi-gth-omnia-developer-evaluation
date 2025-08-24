using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping;

public class ProductConfiguration : BaseEntityConfiguration<Product>
{
    public override void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products");

        builder.Property(p => p.Name)
               .IsRequired()
               .HasMaxLength(200);

        builder.Property(p => p.Description)
               .IsRequired()
               .HasMaxLength(1000);

        builder.Property(p => p.UnitPrice)
               .IsRequired()
               .HasPrecision(10, 2);

        builder.Property(p => p.Category)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(p => p.IsActive)
               .IsRequired()
               .HasDefaultValue(true);
    }
}
