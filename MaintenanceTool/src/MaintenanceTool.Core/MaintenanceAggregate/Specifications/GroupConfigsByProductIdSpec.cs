using Ardalis.Specification;

namespace MaintenanceTool.Core.MaintenanceAggregate.Specifications;

public class GroupConfigsByProductIdSpec : Specification<GroupConfig>
{
  public GroupConfigsByProductIdSpec(int productId)
  {
    Query
      .Where(gc => gc.ProductId == productId)
      .Include(gc => gc.SparePart);
  }
}
