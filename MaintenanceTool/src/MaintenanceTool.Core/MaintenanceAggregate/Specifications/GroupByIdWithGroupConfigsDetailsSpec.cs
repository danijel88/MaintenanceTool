using Ardalis.Specification;

namespace MaintenanceTool.Core.MaintenanceAggregate.Specifications;

public class GroupByIdWithGroupConfigsDetailsSpec : Specification<Group>
{
  public GroupByIdWithGroupConfigsDetailsSpec(int id)
  {
    Query
      .Where(w => w.Id == id)
      .Include(grp => grp.GroupConfigs)
      .ThenInclude(gc => gc.Product)
      .Include(grp => grp.GroupConfigs)
      .ThenInclude(gc => gc.SparePart);

  }
}


