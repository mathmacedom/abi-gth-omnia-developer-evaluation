using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping;

public class SaleConfiguration : BaseEntityConfiguration<Sale>
{
    public override void Configure(EntityTypeBuilder<Sale> builder)
    {
        builder.ToTable("Sales");

        builder.Property(s => s.SaleNumber).IsRequired().HasMaxLength(50);
        builder.Property(s => s.SaleDate).IsRequired();
        builder.Property(s => s.CustomerId).IsRequired().HasColumnType("uuid");
        builder.Property(s => s.CustomerName).IsRequired();
        builder.Property(s => s.BranchId).IsRequired().HasColumnType("uuid");
        builder.Property(s => s.BranchName).IsRequired();
        builder.Property(s => s.IsCancelled).IsRequired().HasColumnType("boolean");
        builder.Property(s => s.TotalAmount).IsRequired().HasColumnType("decimal(10,2)");
        builder.Property(s => s.TotalDiscount).IsRequired().HasColumnType("decimal(10,2)");
        builder.Property(x => x.CancelledAt).IsRequired(false).HasColumnType("timestamp with time zone");

        builder.OwnsMany(s => s.Items, itemBuilder =>
        {
            itemBuilder.ToTable("SaleItems");
            itemBuilder.Property(si => si.ProductId).IsRequired().HasColumnType("uuid");
            itemBuilder.Property(si => si.ProductName).IsRequired();
            itemBuilder.Property(si => si.Quantity).IsRequired().HasColumnType("int");
            itemBuilder.Property(si => si.UnitPrice).IsRequired().HasColumnType("decimal(10,2)");
            itemBuilder.Property(si => si.Discount).IsRequired().HasColumnType("decimal(10,2)");
            itemBuilder.Property(si => si.Total).IsRequired().HasColumnType("decimal(10,2)");
            itemBuilder.Property(si => si.IsCancelled).IsRequired().HasColumnType("boolean");
        });
    }
}
