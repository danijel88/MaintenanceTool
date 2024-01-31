using Ardalis.GuardClauses;
using MaintenanceTool.Core.MaintenanceAggregate;
using MaintenanceTool.SharedKernel;
using MaintenanceTool.SharedKernel.Interfaces;

namespace MaintenanceTool.Core.CompanyAggregate;

public class Plant : EntityBase, IAggregateRoot
{
  private readonly List<Group> _groups = new();

  public Plant(string plantName, int companyId)
  {
    PlantName = Guard.Against.NullOrEmpty(plantName, nameof(plantName));
    CompanyId = Guard.Against.NegativeOrZero(companyId, nameof(companyId));
  }

  public string PlantName { get; private set; }
  public int CompanyId { get; private set; }
  public IEnumerable<Group> Groups => _groups.AsReadOnly();
}
