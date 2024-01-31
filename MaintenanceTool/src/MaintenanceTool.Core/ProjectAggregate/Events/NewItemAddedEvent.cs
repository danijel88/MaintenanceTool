using MaintenanceTool.SharedKernel;

namespace MaintenanceTool.Core.ProjectAggregate.Events;

public class NewItemAddedEvent : DomainEventBase
{
  public NewItemAddedEvent(Project project,
    ToDoItem newItem)
  {
    Project = project;
    NewItem = newItem;
  }

  public ToDoItem NewItem { get; set; }
  public Project Project { get; set; }
}
