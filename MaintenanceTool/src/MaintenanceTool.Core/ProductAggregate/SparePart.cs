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

  public string SparePartName { get; private set; }
  public string Description { get; private set; }
  public string SapCode { get; private set; }
  public int CompanyId { get; private set; }
  public string DisplayedName => $"{SparePartName} - {Description}";
  private readonly List<SparePartImage> _sparePartImages = new();
  public IEnumerable<SparePartImage> SparePartImages => _sparePartImages.AsReadOnly();

  public void SetDescription(string description)
  {
    Description = description;
  }

  public void SetSapCode(string sapCode)
  {
    Guard.Against.NullOrEmpty(sapCode);
    SapCode = sapCode;
  }

  public void SetName(string sparePartName)
  {
    Guard.Against.NullOrEmpty(sparePartName);
    SparePartName = sparePartName;
  }

  public void UploadImage(SparePartImage image)
  {
    _sparePartImages.Add(image);
  }
}
