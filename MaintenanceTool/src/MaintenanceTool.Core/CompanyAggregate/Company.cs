using Ardalis.GuardClauses;
using MaintenanceTool.SharedKernel;
using MaintenanceTool.SharedKernel.Interfaces;

namespace MaintenanceTool.Core.CompanyAggregate;

public class Company : EntityBase, IAggregateRoot
{
  private readonly List<Plant> _plants = new();

  public Company(string companyName)
  {
    CompanyName = Guard.Against.NullOrEmpty(companyName, nameof(companyName));
  }

  public string CompanyName { get; private set; }
  public IEnumerable<Plant> Plants => _plants.AsReadOnly();

  public void AddPlant(Plant plant)
  {
    Guard.Against.Null(plant, nameof(plant));
    _plants.Add(plant);
  }
}
