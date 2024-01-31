using System.ComponentModel.DataAnnotations;

namespace MaintenanceTool.Web.ViewModels;

public class CreateSparePartViewModel
{
  [Required] public string SparePartName { get; set; }

  public string Description { get; set; }
  public string SapCode { get; set; }
}
