using MaintenanceTool.Core.ProjectAggregate;
using MaintenanceTool.Core.ProjectAggregate.Specifications;
using MaintenanceTool.SharedKernel.Interfaces;
using MaintenanceTool.Web.ApiModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MaintenanceTool.Web.Pages.ProjectDetails;

public class IndexModel : PageModel
{
  private readonly IRepository<Project> _repository;

  public IndexModel(IRepository<Project> repository)
  {
    _repository = repository;
  }

  [BindProperty(SupportsGet = true)] public int ProjectId { get; set; }

  public string Message { get; set; } = "";

  public ProjectDTO? Project { get; set; }

  public async Task OnGetAsync()
  {
    var projectSpec = new ProjectByIdWithItemsSpec(ProjectId);
    var project = await _repository.FirstOrDefaultAsync(projectSpec);
    if (project == null)
    {
      Message = "No project found.";

      return;
    }

    Project = new ProjectDTO
    (
      project.Id,
      project.Name,
      project.Items
        .Select(item => ToDoItemDTO.FromToDoItem(item))
        .ToList()
    );
  }
}
