namespace MaintenanceTool.Web.ViewModels;

public class ProductViewModel : CreateProductViewModel
{
  public int Id { get; private set; }
  public ProductViewModel(string description, string model, string productName, string serialNumber,int id)
  {
    
    Description = description;
    Model = model;
    ProductName = productName;
    SerialNumber = serialNumber;
    Id = id;
  }
}
