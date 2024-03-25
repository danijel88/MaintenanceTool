using System.ComponentModel.DataAnnotations;

namespace MaintenanceTool.Web.ViewModels;

public class CreateDetailTransactionViewModel
{
  [Required] [DataType("decimal(5,3)")] public decimal Quantity { get; set; }

  [Range(1, Int32.MaxValue)] public int GroupConfigId { get; set; }
  public string MoveService { get; set; }
}
