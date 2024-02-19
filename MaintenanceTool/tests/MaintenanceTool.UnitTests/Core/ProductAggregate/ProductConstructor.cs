using JetBrains.Annotations;
using MaintenanceTool.Core.ProductAggregate;
using Xunit;

namespace MaintenanceTool.UnitTests.Core.ProductAggregate;

[TestSubject(typeof(Product))]
public class ProductConstructor
{
  private string _testProductSerialNumber = "005522AX";
  private string _testProductModel = "beta Model";
  private int _testCompanyId = 1;
  private string _testProductDescription = "Kompresor vazduha";
  private string _testProductName = "Kompresor A450";
  private Product? _testProduct;

  private Product CreateProduct()
  {
    return new Product(_testCompanyId, _testProductDescription, _testProductModel, _testProductName,_testProductSerialNumber,1,DateTime.Today);
  }

  [Fact]
  public void ProductInitializeName()
  {
    _testProduct = CreateProduct();
    Assert.Equal(_testProductName,_testProduct.ProductName);
  }
  [Fact]
  public void ProductInitializeModel()
  {
    _testProduct = CreateProduct();
    Assert.Equal(_testProductModel,_testProduct.Model);
  }
  [Fact]
  public void ProductInitializeSerialNumber()
  {
    _testProduct = CreateProduct();
    Assert.Equal(_testProductSerialNumber,_testProduct.SerialNumber);
  }
  [Fact]
  public void ProductInitializeCompanyId()
  {
    _testProduct = CreateProduct();
    Assert.Equal(_testCompanyId,_testProduct.CompanyId);
  }
  [Fact]
  public void ProductInitializeDescription()
  {
    _testProduct = CreateProduct();
    Assert.Equal(_testProductDescription,_testProduct.Description);
  }
  
}
