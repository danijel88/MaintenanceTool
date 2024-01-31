using System.ComponentModel.DataAnnotations;

namespace MaintenanceTool.Web.ViewModels;

public class CreateGroupViewModel
{
  public List<CreateGroupConfigViewModel> GroupConfigs;

  public CreateGroupViewModel()
  {
    GroupConfigs = new List<CreateGroupConfigViewModel>();
  }

  [Required(ErrorMessage = "System Name can not be empty.")]
  public string GroupName { get; set; }
}
