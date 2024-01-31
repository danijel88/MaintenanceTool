using MaintenanceTool.Core.ProductAggregate;
using MaintenanceTool.Core.ProductAggregate.Specifications;
using MaintenanceTool.SharedKernel.Interfaces;
using MaintenanceTool.Web.Extensions;
using MaintenanceTool.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace MaintenanceTool.Web.Controllers;

[Route("[controller]")]
public class SparePartController : Controller
{
  private readonly IRepository<SparePart> _sparePartRepository;

  public SparePartController(IRepository<SparePart> sparePartRepository)
  {
    _sparePartRepository = sparePartRepository;
  }

  [HttpGet("{companyId}")]
  public async Task<IActionResult> Index(int companyId)
  {
    ViewBag.CompanyId = companyId;
    var sparePartSpecification = new SparePartsByCompanyIdSpec(companyId);
    var spareParts = (await _sparePartRepository.ListAsync(sparePartSpecification))
      .Select(sp => new SparePartViewModel(sp.Description, sp.SapCode, sp.SparePartName))
      .ToList();
    return View(spareParts);
  }

  [HttpGet("{companyId}/Create")]
  public IActionResult Create(int companyId)
  {
    return View();
  }

  [HttpPost("{companyId}/Create")]
  public async Task<IActionResult> Create(int companyId, CreateSparePartViewModel request)
  {
    if (!ModelState.IsValid)
    {
      return View();
    }

    await _sparePartRepository.AddAsync(new SparePart(companyId, request.Description, request.SapCode,
      request.SparePartName));
    var saved = await _sparePartRepository.SaveChangesAsync();
    if (saved == 0)
    {
      TempData["alerts"] = this.ViewAlert(AlertType.Success, "Spare part has been created.");
    }
    else
    {
      return View();
    }

    return RedirectToAction("Index", new { companyId });
  }
}
