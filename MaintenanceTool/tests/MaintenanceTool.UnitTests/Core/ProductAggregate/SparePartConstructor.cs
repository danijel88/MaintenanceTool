using JetBrains.Annotations;
using MaintenanceTool.Core.ProductAggregate;
using Xunit;

namespace MaintenanceTool.UnitTests.Core.ProductAggregate;

[TestSubject(typeof(SparePart))]
public class SparePartConstructor
{
  private int _testCompanyId = 1;
  private string _testSparePartDescription = "some description";
  private string _testSparePartSapCode = "LP00001";
  private string _testSparePartName = "Filter vazduha";
  private SparePart? _testSparePart;

  private SparePart CreateSparePart()
  {
    return new SparePart(_testCompanyId, _testSparePartDescription, _testSparePartSapCode, _testSparePartName);
  }

  [Fact]
  public void SparePartInitializesCompanyId()
  {
    _testSparePart= CreateSparePart();
    Assert.Equal(_testCompanyId,_testSparePart.CompanyId);
  }
  [Fact]
  public void SparePartInitializesName()
  {
    _testSparePart= CreateSparePart();
    Assert.Equal(_testSparePartName,_testSparePart.SparePartName);
  }
  [Fact]
  public void SparePartInitializesDescription()
  {
    _testSparePart= CreateSparePart();
    Assert.Equal(_testSparePartDescription,_testSparePart.Description);
  }
  [Fact]
  public void SparePartInitializesSapCode()
  {
    _testSparePart= CreateSparePart();
    Assert.Equal(_testSparePartSapCode,_testSparePart.SapCode);
  }
  
}
