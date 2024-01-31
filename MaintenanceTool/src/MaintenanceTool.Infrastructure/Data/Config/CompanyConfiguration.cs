using MaintenanceTool.Core.CompanyAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MaintenanceTool.Infrastructure.Data.Config;

public class CompanyConfiguration : IEntityTypeConfiguration<Company>
{
  public void Configure(EntityTypeBuilder<Company> builder)
  {
    builder.Property(c => c.CompanyName)
      .HasMaxLength(100)
      .IsRequired();
  }
}
