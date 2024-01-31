using Ardalis.Specification;

namespace MaintenanceTool.Core.CompanyAggregate.Specifications;

public class CompaniesWithPlantsSpec : Specification<Company>
{
  public CompaniesWithPlantsSpec()
  {
    Query
      .Include(company => company.Plants)
      .ThenInclude(plant => plant.Groups);
  }
}
