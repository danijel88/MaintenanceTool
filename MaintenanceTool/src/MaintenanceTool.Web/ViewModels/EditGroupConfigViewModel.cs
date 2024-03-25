namespace MaintenanceTool.Web.ViewModels;

public class EditGroupConfigViewModel
{
  public EditGroupConfigViewModel()
  {
    GroupConfigs = new List<CreateGroupConfigViewModel>();
    Response = new List<GroupDetailsViewModel>();
  }

  public List<GroupDetailsViewModel> Response { get; set; }
  public List<CreateGroupConfigViewModel> GroupConfigs { get; set; }
}
