using Ardalis.GuardClauses;
using MaintenanceTool.SharedKernel;
using MaintenanceTool.SharedKernel.Interfaces;

namespace MaintenanceTool.Core.ProductAggregate;

public class ProductImage : EntityBase
{
  public ProductImage(int productId, string path)
  {
    ProductId = Guard.Against.NegativeOrZero(productId,nameof(productId));
    Path = Guard.Against.NullOrEmpty(path,nameof(path));
  }

  public int ProductId { get; private set; }
  public string Path { get; private set; }
}
