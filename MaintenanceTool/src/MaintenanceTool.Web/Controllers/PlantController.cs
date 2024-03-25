using MaintenanceTool.Core.CompanyAggregate;
using MaintenanceTool.SharedKernel.Interfaces;
using MaintenanceTool.Web.Extensions;
using MaintenanceTool.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MaintenanceTool.Web.Controllers;

[Route("[controller]/[action]")]
[Authorize(Roles = "Admin,User")]
public class PlantController : Controller
{
  private readonly IRepository<Company> _companyRepository;
  private readonly IRepository<Plant> _plantRepository;

  public PlantController(IRepository<Plant> plantRepository, IRepository<Company> companyRepository)
  {
    _plantRepository = plantRepository;
    _companyRepository = companyRepository;
  }

  [HttpGet]
  public async Task<IActionResult> Create()
  {
    ViewBag.Companies = new SelectList(await _companyRepository.ListAsync(), "Id", "CompanyName");
    return View();
  }

  [HttpPost]
  public async Task<IActionResult> Create(CreatePlantViewModel requests)
  {
    if (!ModelState.IsValid)
    {
      ViewBag.Companies = new SelectList(await _companyRepository.ListAsync(), "Id", "CompanyName");
      return View();
    }

    await _plantRepository.AddAsync(new Plant(requests.PlantName, requests.CompanyId));
    var saved = await _plantRepository.SaveChangesAsync();
    if (saved == 0)
    {
      TempData["alerts"] = this.ViewAlert(AlertType.Success, "Plant saved.");
    }

    return RedirectToAction("Create");
  }
}
