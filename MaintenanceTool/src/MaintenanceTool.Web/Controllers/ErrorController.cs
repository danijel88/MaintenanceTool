using Microsoft.AspNetCore.Mvc;

namespace MaintenanceTool.Web.Controllers;

public class ErrorController : Controller
{
  [Route("/Error/Forbidden")]
  public IActionResult Forbidden()
  {
    return View();
  }
}
