using JetBrains.Annotations;
using MaintenanceTool.Core.InventoryAggregate;
using Xunit;

namespace MaintenanceTool.UnitTests.Core.InventoryAggregate;

[TestSubject(typeof(Transaction))]
public class TransactionConstructor
{
  private DateTime _testCreateDate = new DateTime(2023, 12, 06);
  private string _testDoneBy = "Danijel";
  private MaintenanceType _testMaintenanceType = MaintenanceType.Planned;
  private int _testPlantId = 1;
  private int _testGroupConfigId = 1;
  private decimal _testQuantity = 2m;
  private TransactionType _testTransactionType = TransactionType.Receive;
  private Transaction? _testTransaction;
  private string _description;

  private Transaction CreateTransaction()
  {
    return new Transaction(_testCreateDate,_testDoneBy, _testMaintenanceType, _testPlantId, _testGroupConfigId, _testQuantity,_testTransactionType,_description);
  }
  [Fact]
  public void InitializeTransactionCreateDate()
  {
    _testTransaction = CreateTransaction();
    Assert.Equal(_testCreateDate,_testTransaction.CreatedDate);
  }
  [Fact]
  public void InitializeTransactionDoneBy()
  {
    _testTransaction = CreateTransaction();
    Assert.Equal(_testDoneBy,_testTransaction.DoneBy);
  }

  [Fact]
  public void InitializeTransactionMaintenanceType()
  {
    _testTransaction = CreateTransaction();
    Assert.Equal(_testMaintenanceType,_testTransaction.MaintenanceType);
  }
  [Fact]
  public void InitializeTransactionPlantId()
  {
    _testTransaction = CreateTransaction();
    Assert.Equal(_testPlantId,_testTransaction.PlantId);
  }
  [Fact]
  public void InitializeTransactionGroupConfigId()
  {
    _testTransaction = CreateTransaction();
    Assert.Equal(_testGroupConfigId, _testTransaction.GroupConfigId);
  }
  [Fact]
  public void InitializeTransactionQuantity()
  {
    _testTransaction = CreateTransaction();
    Assert.Equal(_testQuantity,_testTransaction.Quantity);
  }

  [Fact]
  public void InitializeTransactionTransactionType()
  {
    _testTransaction = CreateTransaction();
    Assert.Equal(_testTransactionType,_testTransaction.TransactionType);
  }
  
  
}
