using MaintenanceTool.Core.InventoryAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MaintenanceTool.Infrastructure.Data.Config;

public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
{
  public void Configure(EntityTypeBuilder<Transaction> builder)
  {
    builder.Property(t => t.DoneBy)
      .HasMaxLength(250)
      .IsRequired();
    builder.Property(t => t.CreatedDate)
      .IsRequired();
    builder.Property(t => t.Quantity)
      .HasColumnType("decimal(5,3)")
      .IsRequired();
  }
}
