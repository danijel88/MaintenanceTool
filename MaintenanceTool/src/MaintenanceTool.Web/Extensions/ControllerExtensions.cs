using Microsoft.AspNetCore.Mvc;

namespace MaintenanceTool.Web.Extensions;

public static class ControllerExtensions
{
  //scripted by gunesekrem.com
  public static string ViewAlert(this Controller controller, AlertType alert = AlertType.Info,
    string alertMessage = "Info")
  {
    if (alert == AlertType.Success)
    {
      return $"<span class='successAlert'>{alertMessage}</span>";
    }

    if (alert == AlertType.Warning)
    {
      return $"<span class='warningAlert'>{alertMessage}</span>";
    }

    if (alert == AlertType.Error)
    {
      return $"<span class='errorAlert'>{alertMessage}</span>";
    }

    return $"<span class='infoAlert'>{alertMessage}</span>";
  }
}
