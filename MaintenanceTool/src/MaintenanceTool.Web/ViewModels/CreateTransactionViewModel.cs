using System.ComponentModel.DataAnnotations;

namespace MaintenanceTool.Web.ViewModels;

public class CreateTransactionViewModel
{
  public List<CreateDetailTransactionViewModel> DetailTransactions;

  public CreateTransactionViewModel()
  {
    DetailTransactions = new List<CreateDetailTransactionViewModel>();
  }

  public DateTime CreatedDate { get; set; }

  [Range(1, Int32.MaxValue)] public int TransactionType { get; set; }

  [Range(1, Int32.MaxValue)] public int MaintenanceType { get; set; }

  [Required] public string DoneBy { get; set; }

  [Range(1, Int32.MaxValue)] public int PlantId { get; set; }
  public string Description { get; set; }
}
