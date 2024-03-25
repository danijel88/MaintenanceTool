namespace MaintenanceTool.Web.ViewModels;

public class EditProductViewModel 
{
  public string Description { get; set; }
  public string Model { get; set; }
  public string ProductName { get; set; }
  public string SerialNumber { get; set; }
  public int Id { get; set; }
  public int CompanyId { get; set; }
  public int EachMonths { get; set; }
}
