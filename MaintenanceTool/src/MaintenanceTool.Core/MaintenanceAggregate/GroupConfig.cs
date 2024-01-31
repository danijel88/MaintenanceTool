using Ardalis.GuardClauses;
using MaintenanceTool.Core.ProductAggregate;
using MaintenanceTool.SharedKernel;

namespace MaintenanceTool.Core.MaintenanceAggregate;

public class GroupConfig : EntityBase
{
  public GroupConfig(int groupId, int productId, int sparePartId)
  {
    GroupId = Guard.Against.NegativeOrZero(groupId, nameof(groupId));
    ProductId = Guard.Against.NegativeOrZero(productId, nameof(productId));
    SparePartId = Guard.Against.NegativeOrZero(sparePartId, nameof(sparePartId));
  }

  public int ProductId { get; private set; }
  public int SparePartId { get; private set; }
  public Product Product { get; private set; }
  public SparePart SparePart { get; private set; }
  public int GroupId { get; private set; }
}
