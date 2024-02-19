using System.ComponentModel.DataAnnotations;

namespace MaintenanceTool.Web.ViewModels;

public class CreateProductViewModel
{
  [Required] public string ProductName { get; set; }

  [Required] public string SerialNumber { get; set; }

  [Required] public string Model { get; set; }

  public string Description { get; set; }
  public int CompanyId { get; set; }
  public int EachMonths { get; set; }
  [DataType(DataType.Date)]
  public DateTime LastService { get; set; }
}
