using MaintenanceTool.Core.CompanyAggregate;
using MaintenanceTool.SharedKernel.Interfaces;
using MaintenanceTool.Web.Extensions;
using MaintenanceTool.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MaintenanceTool.Web.Controllers;

[Route("[controller]")]
[Authorize(Roles = "Admin")]
public class CompanyController : Controller
{
  private readonly IRepository<Company> _companyRepository;

  public CompanyController(IRepository<Company> companyRepository)
  {
    _companyRepository = companyRepository;
  }

  [HttpGet("Create")]
  public IActionResult Create()
  {
    return View();
  }


  [HttpPost("Create")]
  public async Task<IActionResult> Create(CreateCompanyViewModel request)
  {
    if (string.IsNullOrEmpty(request.CompanyName))
    {
      return View();
    }

    await _companyRepository.AddAsync(new Company(request.CompanyName));
    var saved = await _companyRepository.SaveChangesAsync();
    if (saved == 0)
    {
      TempData["alerts"] = this.ViewAlert(AlertType.Success, "Company saved.");
    }

    return RedirectToAction("Create");
  }
}
