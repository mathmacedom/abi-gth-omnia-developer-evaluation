using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping;

public class CartConfiguration : BaseEntityConfiguration<Cart>
{
    public override void Configure(EntityTypeBuilder<Cart> builder)
    {
        builder.ToTable("Carts");

        builder.Property(s => s.Branch).IsRequired().HasMaxLength(200);
        builder.Property(s => s.CustomerId).IsRequired().HasColumnType("uuid");
        builder.Property(s => s.Branch).IsRequired();
        builder.Property(u => u.Status)
            .HasConversion<string>()
            .HasMaxLength(20);

        builder.OwnsMany(s => s.Items, itemBuilder =>
        {
            itemBuilder.ToTable("CartItems");
            itemBuilder.Property(si => si.ProductId).IsRequired().HasColumnType("uuid");
            itemBuilder.Property(si => si.Quantity).IsRequired().HasColumnType("int");
            itemBuilder.Property(si => si.UnitPrice).IsRequired().HasColumnType("decimal(10,2)");
            itemBuilder.Property(si => si.Subtotal).IsRequired().HasColumnType("decimal(10,2)");
        });
    }
}
