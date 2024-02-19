using MaintenanceTool.Core.ProductAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MaintenanceTool.Infrastructure.Data.Config;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
  public void Configure(EntityTypeBuilder<Product> builder)
  {
    builder.Property(p => p.Model)
      .HasMaxLength(100)
      .IsRequired();
    builder.Property(p => p.Description)
      .HasMaxLength(250);
    builder.Property(p => p.ProductName)
      .HasMaxLength(100)
      .IsRequired();
    builder.Property(p => p.SerialNumber)
      .HasMaxLength(50)
      .IsRequired();
    builder.Property(p=>p.LastService)
      .HasColumnType("DATE")
      .HasDefaultValue(DateTime.Today);
    builder.Property(p=>p.NextService)
      .HasColumnType("DATE")
      .HasDefaultValue(DateTime.Today);
  }
}
