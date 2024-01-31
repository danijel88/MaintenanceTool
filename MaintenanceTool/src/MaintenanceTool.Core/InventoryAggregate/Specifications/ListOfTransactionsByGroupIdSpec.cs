using Ardalis.Specification;

namespace MaintenanceTool.Core.InventoryAggregate.Specifications;

public class ListOfTransactionsByGroupIdSpec : Specification<Transaction>
{
  public ListOfTransactionsByGroupIdSpec(int groupId, int plantId)
  {
    Query
      .Include(transaction => transaction.GroupConfig)
      .ThenInclude(gc => gc.SparePart)
      .Include(tr => tr.GroupConfig)
      .ThenInclude(gc => gc.Product)
      .Where(w => w.GroupConfig.GroupId == groupId && w.PlantId == plantId && w.TransactionType == TransactionType.Consume);
  }
}
