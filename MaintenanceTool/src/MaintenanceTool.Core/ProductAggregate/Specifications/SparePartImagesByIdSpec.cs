using Ardalis.Specification;

namespace MaintenanceTool.Core.ProductAggregate.Specifications;

public class SparePartImagesByIdSpec : Specification<SparePart>
{
  public SparePartImagesByIdSpec(int sparePartId)
  {
    Query.Where(w => w.Id == sparePartId)
      .Include(w => w.SparePartImages);
  }
}
