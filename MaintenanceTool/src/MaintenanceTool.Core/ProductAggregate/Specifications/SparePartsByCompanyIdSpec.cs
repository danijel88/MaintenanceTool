using Ardalis.Specification;

namespace MaintenanceTool.Core.ProductAggregate.Specifications;

public class SparePartsByCompanyIdSpec : Specification<SparePart>
{
  public SparePartsByCompanyIdSpec(int companyId)
  {
    Query.Where(sparePart => sparePart.CompanyId == companyId);
  }
}
