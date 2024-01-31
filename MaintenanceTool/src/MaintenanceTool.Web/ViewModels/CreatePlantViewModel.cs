using System.ComponentModel.DataAnnotations;

namespace MaintenanceTool.Web.ViewModels;

public class CreatePlantViewModel
{
  [Required] [MaxLength(100)] public string PlantName { get; set; }

  [Range(1, Int32.MaxValue)] public int CompanyId { get; set; }
}
