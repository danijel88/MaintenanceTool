using Ardalis.GuardClauses;
using MaintenanceTool.Core.MaintenanceAggregate;
using MaintenanceTool.SharedKernel;
using MaintenanceTool.SharedKernel.Interfaces;

namespace MaintenanceTool.Core.InventoryAggregate;

public class Transaction : EntityBase, IAggregateRoot
{
  public Transaction(DateTime createdDate, string doneBy, MaintenanceType maintenanceType, int plantId,
    int groupConfigId, decimal quantity, TransactionType transactionType, string description)
  {
    CreatedDate = Guard.Against.Null(createdDate, nameof(createdDate));
    DoneBy = Guard.Against.NullOrEmpty(doneBy, nameof(doneBy));
    MaintenanceType = maintenanceType;
    PlantId = Guard.Against.NegativeOrZero(plantId, nameof(plantId));
    GroupConfigId = groupConfigId;
    Quantity = quantity;
    TransactionType = transactionType;
    Description = description;
  }

  public DateTime CreatedDate { get; private set; }
  public TransactionType TransactionType { get; private set; }
  public MaintenanceType MaintenanceType { get; private set; }
  public decimal Quantity { get; private set; }
  public int GroupConfigId { get; private set; }
  public GroupConfig GroupConfig { get; private set; }
  public string DoneBy { get; private set; }
  public int PlantId { get; private set; }
  public string Description { get; private set; }
}
