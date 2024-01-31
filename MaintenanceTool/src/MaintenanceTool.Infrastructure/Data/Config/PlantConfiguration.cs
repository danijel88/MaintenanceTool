using MaintenanceTool.Core.CompanyAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MaintenanceTool.Infrastructure.Data.Config;

public class PlantConfiguration : IEntityTypeConfiguration<Plant>
{
  public void Configure(EntityTypeBuilder<Plant> builder)
  {
    builder.Property(p => p.PlantName)
      .HasMaxLength(100)
      .IsRequired();
  }
}
