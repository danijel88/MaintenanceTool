using System.Diagnostics;
using MaintenanceTool.Core.CompanyAggregate;
using MaintenanceTool.Core.CompanyAggregate.Specifications;
using MaintenanceTool.SharedKernel.Interfaces;
using MaintenanceTool.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace MaintenanceTool.Web.Controllers;

/// <summary>
///   A sample MVC controller that uses views.
///   Razor Pages provides a better way to manage view-based content, since the behavior, viewmodel, and view are all in
///   one place,
///   rather than spread between 3 different folders in your Web project. Look in /Pages to see examples.
///   See: https://ardalis.com/aspnet-core-razor-pages-%E2%80%93-worth-checking-out/
/// </summary>
public class HomeController : Controller
{
  private readonly IRepository<Company> _companyRepository;

  public HomeController(IRepository<Company> companyRepository)
  {
    _companyRepository = companyRepository;
  }

  public async Task<IActionResult> Index()
  {
    var companiesWithPlantsSpec = new CompaniesWithPlantsSpec();
    var response = await _companyRepository.ListAsync(companiesWithPlantsSpec);
    return View(response);
  }

  [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
  public IActionResult Error()
  {
    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
  }
}
