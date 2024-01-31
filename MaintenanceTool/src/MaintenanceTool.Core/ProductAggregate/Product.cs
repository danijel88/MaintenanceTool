using Ardalis.GuardClauses;
using MaintenanceTool.SharedKernel;
using MaintenanceTool.SharedKernel.Interfaces;

namespace MaintenanceTool.Core.ProductAggregate;

public class Product : EntityBase, IAggregateRoot
{
  public Product(int companyId, string description, string model, string productName, string serialNumber)
  {
    CompanyId = Guard.Against.NegativeOrZero(companyId, nameof(companyId));
    Description = description;
    Model = Guard.Against.NullOrEmpty(model, nameof(model));
    ProductName = Guard.Against.NullOrEmpty(productName, nameof(productName));
    SerialNumber = Guard.Against.NullOrEmpty(serialNumber, nameof(serialNumber));
  }

  public string ProductName { get; }
  public string SerialNumber { get; private set; }
  public string Model { get; private set; }
  public string Description { get; }
  public int CompanyId { get; private set; }

  public string DisplayedName => $"{ProductName} - {Description}";
}
