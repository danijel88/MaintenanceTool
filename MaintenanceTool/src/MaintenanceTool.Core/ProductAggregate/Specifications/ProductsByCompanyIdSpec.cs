using Ardalis.Specification;

namespace MaintenanceTool.Core.ProductAggregate.Specifications;

public class ProductsByCompanyIdSpec : Specification<Product>
{
  public ProductsByCompanyIdSpec(int companyId)
  {
    Query
      .Where(w => w.CompanyId == companyId);
  }
}
