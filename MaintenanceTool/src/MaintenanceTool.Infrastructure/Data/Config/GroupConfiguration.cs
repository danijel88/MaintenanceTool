using MaintenanceTool.Core.MaintenanceAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MaintenanceTool.Infrastructure.Data.Config;

public class GroupConfiguration : IEntityTypeConfiguration<Group>
{
  public void Configure(EntityTypeBuilder<Group> builder)
  {
    builder.Property(g => g.GroupName)
      .HasMaxLength(100)
      .IsRequired();
  }
}
