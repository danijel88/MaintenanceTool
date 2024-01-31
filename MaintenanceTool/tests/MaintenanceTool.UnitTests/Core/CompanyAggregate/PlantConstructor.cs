using JetBrains.Annotations;
using MaintenanceTool.Core.CompanyAggregate;
using Xunit;

namespace MaintenanceTool.UnitTests.Core.CompanyAggregate;

[TestSubject(typeof(Plant))]
public class PlantConstructor
{
  private string _testPlantName = "Plant name";
  private int _testCompanyId = 1;
  private Plant _testPlant;

  private Plant CreatePlant()
  {
    return new Plant(_testPlantName, _testCompanyId);
  }

  [Fact]
  public void InitializePlantName()
  {
    _testPlant = CreatePlant();
    Assert.Equal(_testPlantName,_testPlant.PlantName);
  }

  [Fact]
  public void InitializeCompanyId()
  {
    _testPlant = CreatePlant();
    Assert.Equal(_testCompanyId,_testPlant.CompanyId);
  }
}
