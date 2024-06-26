﻿using System.ComponentModel.DataAnnotations;

namespace MaintenanceTool.Web.ViewModels;

public class LoginViewModel
{
  [Required]
  public string Email { get; set; }

  [Required]
  public string Password { get; set; }
}
