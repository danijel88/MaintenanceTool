using Ardalis.GuardClauses;
using MaintenanceTool.SharedKernel;
using MaintenanceTool.SharedKernel.Interfaces;

namespace MaintenanceTool.Core.ProductAggregate;

public class SparePart : EntityBase, IAggregateRoot
{
  public SparePart(int companyId, string description, string sapCode, string sparePartName)
  {
    CompanyId = Guard.Against.NegativeOrZero(companyId, nameof(companyId));
    Description = description;
    SapCode = Guard.Against.NullOrEmpty(sapCode, nameof(sapCode));
    SparePartName = Guard.Against.NullOrEmpty(sparePartName, nameof(sparePartName));
  }

  public string SparePartName { get; }
  public string Description { get; }
  public string SapCode { get; private set; }
  public int CompanyId { get; private set; }
  public string DisplayedName => $"{SparePartName} - {Description}";
}
