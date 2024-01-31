namespace MaintenanceTool.Web.ViewModels;

public class SparePartViewModel : CreateSparePartViewModel
{
  public SparePartViewModel(string description, string sapCode, string sparePartName)
  {
    Description = description;
    SapCode = sapCode;
    SparePartName = sparePartName;
  }
}
