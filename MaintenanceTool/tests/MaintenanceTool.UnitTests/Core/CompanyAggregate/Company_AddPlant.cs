using MaintenanceTool.Core.CompanyAggregate;
using Xunit;

namespace MaintenanceTool.UnitTests.Core.CompanyAggregate;

public class Company_AddPlant
{
  private Company _testCompany = new Company("Test Company Name");

  [Fact]
  public void AddsPlantToPlants()
  {
    var _testPlant = new Plant("Some plant name", 1);
    _testCompany.AddPlant(_testPlant);
    Assert.Contains(_testPlant, _testCompany.Plants);
  }

  [Fact]
  public void ThrowsExceptionGivenNullPlant()
  {
#nullable disable
    Action action = () => _testCompany.AddPlant(null);
#nullable enable
    var ex = Assert.Throws<ArgumentNullException>(action);
    Assert.Equal("plant", ex.ParamName);
  }
}
