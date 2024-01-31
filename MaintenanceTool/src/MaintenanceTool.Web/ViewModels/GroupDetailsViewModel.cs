namespace MaintenanceTool.Web.ViewModels;

public class GroupDetailsViewModel
{
  public GroupDetailsViewModel(int groupConfigId, string product, string sparePart)
  {
    GroupConfigId = groupConfigId;
    Product = product;
    SparePart = sparePart;
  }

  public int GroupConfigId { get; set; }
  public string Product { get; set; }
  public string SparePart { get; set; }
}
