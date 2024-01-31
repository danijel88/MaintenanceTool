namespace MaintenanceTool.Web.ViewModels;

public class ProductViewModel : CreateProductViewModel
{
  public ProductViewModel(string description, string model, string productName, string serialNumber)
  {
    Description = description;
    Model = model;
    ProductName = productName;
    SerialNumber = serialNumber;
  }
}
