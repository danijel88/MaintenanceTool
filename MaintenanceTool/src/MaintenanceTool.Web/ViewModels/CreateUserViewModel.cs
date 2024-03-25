using System.ComponentModel.DataAnnotations;

namespace MaintenanceTool.Web.ViewModels;

public class CreateUserViewModel
{
  [Required]
  [DataType(DataType.EmailAddress)]
  public string Email { get; set; }

  [Required]
  [DataType(DataType.Password)]
  public string Password { get; set; }

  [Required]
  [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
  [DataType(DataType.Password)]
  public string ConfirmPassword { get; set; }
}
