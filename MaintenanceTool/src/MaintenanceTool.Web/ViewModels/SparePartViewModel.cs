namespace MaintenanceTool.Web.ViewModels;

public class SparePartViewModel : CreateSparePartViewModel
{
  public int Id { get; private set; }
  public SparePartViewModel(string description, string sapCode, string sparePartName, int id)
  {
    Description = description;
    SapCode = sapCode;
    SparePartName = sparePartName;
    Id = id;
  }
}
