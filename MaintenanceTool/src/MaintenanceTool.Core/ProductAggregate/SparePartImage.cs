using Ardalis.GuardClauses;
using MaintenanceTool.SharedKernel;

namespace MaintenanceTool.Core.ProductAggregate;

public class SparePartImage : EntityBase
{

  public SparePartImage(int sparePartId, string path)
  {
    SparePartId = Guard.Against.NegativeOrZero(sparePartId, nameof(sparePartId));
    Path = Guard.Against.NullOrEmpty(path, nameof(path));
  }

  public int SparePartId { get; private set; }
  public string Path { get; private set; }
}
