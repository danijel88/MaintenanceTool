using Ardalis.GuardClauses;
using Autofac.Core;
using MaintenanceTool.SharedKernel;
using MaintenanceTool.SharedKernel.Interfaces;

namespace MaintenanceTool.Core.MaintenanceAggregate;

public class Group : EntityBase, IAggregateRoot
{
  private readonly List<GroupConfig> _groupConfigs = new();
  private readonly List<ServiceLog> _serviceLogs = new();

  public Group(string groupName, int plantId)
  {
    GroupName = groupName;
    PlantId = plantId;
  }

  public string GroupName { get; private set; }

  public int PlantId { get; private set; }
  public IEnumerable<GroupConfig> GroupConfigs => _groupConfigs.AsReadOnly();
  public IEnumerable<ServiceLog> ServiceLogs => _serviceLogs.AsReadOnly();

  public void AddGroupConfig(GroupConfig groupConfig)
  {
    Guard.Against.Null(groupConfig, nameof(groupConfig));
    _groupConfigs.Add(groupConfig);
  }

  public void AddServiceLog(ServiceLog serviceLog)
  {
    Guard.Against.Null(serviceLog, nameof(serviceLog));
    _serviceLogs.Add(serviceLog);
  }
}
