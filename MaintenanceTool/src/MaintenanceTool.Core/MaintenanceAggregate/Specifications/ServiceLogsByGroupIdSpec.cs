using Ardalis.Specification;

namespace MaintenanceTool.Core.MaintenanceAggregate.Specifications;

public class ServiceLogsByGroupIdSpec : Specification<Group>
{
  public ServiceLogsByGroupIdSpec(int groupId)
  {
    Query
      .Where(w => w.Id == groupId)
      .Include(w=>w.ServiceLogs);
  }
}
