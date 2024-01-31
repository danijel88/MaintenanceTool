using Ardalis.Result;
using MaintenanceTool.Core.Interfaces;
using MaintenanceTool.Core.MaintenanceAggregate;
using MaintenanceTool.Core.MaintenanceAggregate.Specifications;
using MaintenanceTool.SharedKernel.Interfaces;

namespace MaintenanceTool.Core.Services;

public class GroupConfigSearchService : IGroupConfigSearchService
{
  private readonly IRepository<Group> _groupRepository;

  public GroupConfigSearchService(IRepository<Group> groupRepository)
  {
    _groupRepository = groupRepository;
  }

  public async Task<Result<List<GroupConfig>>> GetGroupConfigSearchAsync(int groupId, int productId)
  {
    if (groupId <= 0)
    {
      var errors = new List<ValidationError>
      {
        new() { Identifier = nameof(groupId), ErrorMessage = $"{nameof(groupId)} is not valid." }
      };
      return Result<List<GroupConfig>>.Invalid(errors);
    }

    var groupSpec = new GroupByIdWithGroupConfigsDetailsSpec(groupId);
    var group = await _groupRepository.FirstOrDefaultAsync(groupSpec);
    if (group == null)
    {
      return Result<List<GroupConfig>>.NotFound();
    }

    var groupConfigSpec = new GroupConfigsByProductIdSpec(productId);
    try
    {
      var groupConfigs = groupConfigSpec.Evaluate(group.GroupConfigs).ToList();
      return new Result<List<GroupConfig>>(groupConfigs);
    }
    catch (Exception ex)
    {
      return Result<List<GroupConfig>>.Error(ex.Message);
    }
  }
}
