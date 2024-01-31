using System.ComponentModel.DataAnnotations;

namespace MaintenanceTool.Web.ViewModels;

public class CreateCompanyViewModel
{
  [Required]
  [MaxLength(100, ErrorMessage = "Exceeded number of characters.")]
  public string CompanyName { get; set; }
}
