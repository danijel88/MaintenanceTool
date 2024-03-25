using Ardalis.GuardClauses;
using MaintenanceTool.SharedKernel;
using MaintenanceTool.SharedKernel.Interfaces;

namespace MaintenanceTool.Core.ProductAggregate;

public class Product : EntityBase, IAggregateRoot
{
  public Product(int companyId, string description, string model, string productName, string serialNumber,
    int eachMonths, DateTime lastService)
  {
    CompanyId = Guard.Against.NegativeOrZero(companyId, nameof(companyId));
    Description = description;
    LastService = lastService;
    EachMonths = Guard.Against.Negative(eachMonths, nameof(eachMonths));
    Model = Guard.Against.NullOrEmpty(model, nameof(model));
    ProductName = Guard.Against.NullOrEmpty(productName, nameof(productName));
    SerialNumber = Guard.Against.NullOrEmpty(serialNumber, nameof(serialNumber));
    NextService = LastService.AddMonths(EachMonths);
  }

  public string ProductName { get; private set; }
  public string SerialNumber { get; private set; }
  public string Model { get; private set; }
  public string Description { get; private set; }
  public int CompanyId { get; private set; }
  public int EachMonths { get; private set; }
  public DateTime LastService { get; private set; }
  public DateTime NextService { get; private set; }

  public string DisplayedName => $"{ProductName} - {Description}";

  private readonly List<ProductImage> _productImages = new();
  public IEnumerable<ProductImage> ProductImages => _productImages.AsReadOnly();

  public void UploadImage(ProductImage productImage)
  {
    _productImages.Add(productImage);
  }

  public void UpdateLastService(DateTime lastService)
  {
    LastService = lastService;
    NextService = lastService.AddMonths(EachMonths);
  }

  public void SetModel(string model)
  {
    Guard.Against.NullOrEmpty(model);
    Model = model;
  }

  public void SetDescription(string description)
  {
    Description = description;
  }

  public void SetName(string productName)
  {
    Guard.Against.NullOrEmpty(productName);
    ProductName = productName;
  }

  public void SetSerialNumber(string serialNumber)
  {
    Guard.Against.NullOrEmpty(serialNumber);
    SerialNumber = serialNumber;
  }

  public void SetEachMonths(int eachMonths)
  {
    EachMonths = eachMonths;
  }
}
