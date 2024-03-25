using MaintenanceTool.Core.ProjectAggregate;
using MaintenanceTool.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace MaintenanceTool.Web;

public static class SeedData
{
  public static readonly Project TestProject1 = new("Test Project", PriorityStatus.Backlog);

  public static readonly ToDoItem ToDoItem1 = new()
  {
    Title = "Get Sample Working", Description = "Try to get the sample to build."
  };

  public static readonly ToDoItem ToDoItem2 = new()
  {
    Title = "Review Solution",
    Description = "Review the different projects in the solution and how they relate to one another."
  };

  public static readonly ToDoItem ToDoItem3 = new()
  {
    Title = "Run and Review Tests", Description = "Make sure all the tests run and review what they are doing."
  };
}


