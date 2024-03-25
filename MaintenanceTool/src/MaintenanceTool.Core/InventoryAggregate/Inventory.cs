using MaintenanceTool.Core.ProductAggregate;
using MaintenanceTool.SharedKernel;
using MaintenanceTool.SharedKernel.Interfaces;

namespace MaintenanceTool.Core.InventoryAggregate;

public class Inventory : EntityBase, IAggregateRoot
{
  public decimal Qty { get; private set; }
  public DateTime CreatedAt { get; private set; }
  public string CreatedBy { get; private set; }
  public int SparePartId { get; private set; }
  public SparePart SparePart { get; private set; }

  public Inventory(DateTime createdAt, string createdBy, decimal qty, int sparePartId)
  {
    CreatedAt = createdAt;
    CreatedBy = createdBy;
    Qty = qty;
    SparePartId = sparePartId;
  }
}
