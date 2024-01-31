using MaintenanceTool.Core.CompanyAggregate;
using Xunit;

namespace MaintenanceTool.UnitTests.Core.CompanyAggregate;

public class CompanyConstructor
{
  private readonly string _testCompanyName = "Test Company Name";
  private Company? _testCompany;

  private Company CreateCompany()
  {
    return new Company(_testCompanyName);
  }

  [Fact]
  public void InitializeCompanyName()
  {
    _testCompany = CreateCompany();
    Assert.Equal(_testCompanyName,_testCompany.CompanyName);
  }
}
