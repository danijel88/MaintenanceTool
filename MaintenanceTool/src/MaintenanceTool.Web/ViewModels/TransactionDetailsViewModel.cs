﻿namespace MaintenanceTool.Web.ViewModels;

public class TransactionDetailsViewModel
{
  public string DoneBy { get; }
  public DateTime CreatedDateDate { get; }
  public string MaintenanceType { get; }
  public string ProductDisplayedName { get; }
  public string SparePartDisplayedName { get; }
  public string Description { get; }

  public TransactionDetailsViewModel(string doneBy, DateTime createdDateDate, string maintenanceType, string productDisplayedName, string sparePartDisplayedName, string description)
  {
    DoneBy = doneBy;
    CreatedDateDate = createdDateDate;
    MaintenanceType = maintenanceType;
    ProductDisplayedName = productDisplayedName;
    SparePartDisplayedName = sparePartDisplayedName;
    Description = description;
  }
}
