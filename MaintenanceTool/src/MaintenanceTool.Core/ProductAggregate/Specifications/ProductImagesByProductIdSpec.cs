using Ardalis.Specification;

namespace MaintenanceTool.Core.ProductAggregate.Specifications;

public class ProductImagesByProductIdSpec : Specification<Product>
{
  public ProductImagesByProductIdSpec(int productId)
  {
    Query.Where(w => w.Id == productId)
      .Include(w=>w.ProductImages);
  }
}
