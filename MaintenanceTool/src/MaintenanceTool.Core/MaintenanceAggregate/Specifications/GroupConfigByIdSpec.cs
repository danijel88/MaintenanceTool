using Ardalis.Specification;

namespace MaintenanceTool.Core.MaintenanceAggregate.Specifications;

public class GroupConfigByIdSpec: Specification<Group>
{
  public GroupConfigByIdSpec(int id)
  {
    Query
      .Include(x => x.GroupConfigs)
      .Where(w=>w.Id == id);
  }
}
