using Ardalis.Specification;

namespace MaintenanceTool.Core.InventoryAggregate.Specifications;

public class InventoryListWithSparePartSpec : Specification<Inventory>
{
  public InventoryListWithSparePartSpec()
  {
    Query
      .Include(w => w.SparePart);
  }
}
