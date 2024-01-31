using MaintenanceTool.SharedKernel;

namespace MaintenanceTool.Core.MaintenanceAggregate;

public class ServiceLog : EntityBase
{
  public ServiceLog(string description, string doneBy, DateTime createdDate)
  {
    Description = description;
    DoneBy = doneBy;
    CreatedDate = createdDate;
  }

  public string Description { get; private set; }
  public string DoneBy { get; private set; }
  public DateTime CreatedDate { get; private set; }
  public int GroupId { get; private set; }
}
