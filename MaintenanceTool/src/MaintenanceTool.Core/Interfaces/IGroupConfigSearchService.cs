using Ardalis.Result;
using MaintenanceTool.Core.MaintenanceAggregate;

namespace MaintenanceTool.Core.Interfaces;

public interface IGroupConfigSearchService
{
  Task<Result<List<GroupConfig>>> GetGroupConfigSearchAsync(int groupId, int productId);
}
