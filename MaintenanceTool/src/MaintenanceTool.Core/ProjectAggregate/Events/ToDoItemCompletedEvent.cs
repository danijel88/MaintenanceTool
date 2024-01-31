using MaintenanceTool.SharedKernel;

namespace MaintenanceTool.Core.ProjectAggregate.Events;

public class ToDoItemCompletedEvent : DomainEventBase
{
  public ToDoItemCompletedEvent(ToDoItem completedItem)
  {
    CompletedItem = completedItem;
  }

  public ToDoItem CompletedItem { get; set; }
}
