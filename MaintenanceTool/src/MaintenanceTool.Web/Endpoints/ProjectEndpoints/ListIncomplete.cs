using Ardalis.ApiEndpoints;
using Ardalis.Result;
using MaintenanceTool.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace MaintenanceTool.Web.Endpoints.ProjectEndpoints;

public class
  ListIncomplete : EndpointBaseAsync.WithRequest<ListIncompleteRequest>.WithActionResult<ListIncompleteResponse>
{
  private readonly IToDoItemSearchService _searchService;

  public ListIncomplete(IToDoItemSearchService searchService)
  {
    _searchService = searchService;
  }

  [HttpGet("/Projects/{ProjectId}/IncompleteItems")]
  [SwaggerOperation(
    Summary = "Gets a list of a project's incomplete items",
    Description = "Gets a list of a project's incomplete items",
    OperationId = "Project.ListIncomplete",
    Tags = new[] { "ProjectEndpoints" })
  ]
  public override async Task<ActionResult<ListIncompleteResponse>> HandleAsync(
    [FromQuery] ListIncompleteRequest request,
    CancellationToken cancellationToken = new())
  {
    if (request.SearchString == null)
    {
      return BadRequest();
    }

    var response = new ListIncompleteResponse(0, new List<ToDoItemRecord>());
    var result = await _searchService.GetAllIncompleteItemsAsync(request.ProjectId, request.SearchString);

    if (result.Status == ResultStatus.Ok)
    {
      response.ProjectId = request.ProjectId;
      response.IncompleteItems = new List<ToDoItemRecord>(
        result.Value.Select(
          item => new ToDoItemRecord(item.Id,
            item.Title,
            item.Description,
            item.IsDone)));
    }
    else if (result.Status == ResultStatus.Invalid)
    {
      return BadRequest(result.ValidationErrors);
    }
    else if (result.Status == ResultStatus.NotFound)
    {
      return NotFound();
    }

    return Ok(response);
  }
}
