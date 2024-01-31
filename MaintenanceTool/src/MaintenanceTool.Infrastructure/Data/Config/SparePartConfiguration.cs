using MaintenanceTool.Core.ProductAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MaintenanceTool.Infrastructure.Data.Config;

public class SparePartConfiguration : IEntityTypeConfiguration<SparePart>
{
  public void Configure(EntityTypeBuilder<SparePart> builder)
  {
    builder.Property(sp=>sp.SparePartName)
      .HasMaxLength(100)
      .IsRequired();
    builder.Property(sp => sp.Description)
      .HasMaxLength(250);
    builder.Property(sp => sp.SapCode)
      .HasMaxLength(50)
      .IsRequired();
  }
}
