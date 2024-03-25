using MaintenanceTool.Core.ProductAggregate;
using MaintenanceTool.Core.ProductAggregate.Specifications;
using MaintenanceTool.SharedKernel.Interfaces;
using MaintenanceTool.Web.Extensions;
using MaintenanceTool.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;

namespace MaintenanceTool.Web.Controllers;

[Route("[controller]")]
public class SparePartController : Controller
{
  private readonly IRepository<SparePart> _sparePartRepository;
  private readonly IWebHostEnvironment _webHostEnvironment;

  public SparePartController(IRepository<SparePart> sparePartRepository,IWebHostEnvironment webHostEnvironment)
  {
    _sparePartRepository = sparePartRepository;
    _webHostEnvironment = webHostEnvironment;
  }

  [HttpGet("{companyId}")]
  public async Task<IActionResult> Index(int companyId)
  {
    ViewBag.CompanyId = companyId;
    var sparePartSpecification = new SparePartsByCompanyIdSpec(companyId);
    var spareParts = (await _sparePartRepository.ListAsync(sparePartSpecification))
      .Select(sp => new SparePartViewModel(sp.Description, sp.SapCode, sp.SparePartName,sp.Id))
      .ToList();
    return View(spareParts);
  }

  [Authorize(Roles = "Admin,User")]
  [HttpGet("{companyId}/Create")]
  public IActionResult Create(int companyId)
  {
    return View();
  }
  [Authorize(Roles = "Admin,User")]
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

  [HttpGet("{id}/Edit")]
  public async Task<IActionResult> Edit(int id)
  {
    var spare = await _sparePartRepository.GetByIdAsync(id);
    EditSpareViewModel response =
      new EditSpareViewModel
      {
        Description = spare.Description,
        Id = spare.Id,
        SapCode = spare.SapCode,
        SparePartName = spare.SparePartName
      };

    return View(response);
  }

  [HttpPost("{id}/Edit")]
  public async Task<IActionResult> Edit(int id, EditSpareViewModel request)
  {
    var spare = await _sparePartRepository.GetByIdAsync(id);
    spare.SetDescription(request.Description);
    spare.SetSapCode(request.SapCode);
    spare.SetName(request.SparePartName);
    await _sparePartRepository.UpdateAsync(spare);
    await _sparePartRepository.SaveChangesAsync();
    return RedirectToAction("Index",new { companyId = spare.CompanyId });
  }

  [Authorize(Roles = "Admin,User")]
  [HttpGet("{sparePartId}/Upload")]
  public IActionResult Upload(int sparePartId)
  {
    return View();
  }

  [HttpPost("{sparePartId}/Upload")]
  public async Task<IActionResult> Upload(int sparePartId, IFormFile file)
  {
    if (file != null && file.Length > 0)
    {
      try
      {

        string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "img");
        string uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
        string relativePath = Path.Combine("\\img", uniqueFileName);
        string filePath = Path.Combine(uploadsFolder, uniqueFileName);

        using (FileStream fs = new FileStream(filePath, FileMode.Create))
        {
          file.CopyTo(fs);
        }

        var sparePart = await _sparePartRepository.GetByIdAsync(sparePartId);
        if (sparePart == null)
        {
          ViewBag.Message = "Product not found.";
          return View("Upload");
        }
        else
        {
          sparePart.UploadImage(new SparePartImage(sparePartId, relativePath));
          await _sparePartRepository.UpdateAsync(sparePart);
          ViewBag.Message = "Picture uploaded successfully!";
          return RedirectToAction("Index", "Home");
        }

      }
      catch (Exception ex)
      {
        ViewBag.Message = $"Error: {ex.Message}";
        return View("Upload");
      }
    }
    else
    {
      ViewBag.Message = "Please select a picture to upload.";
      return View("Upload");
    }
  }

  [HttpGet("{sparePartId}/Images")]
  public async Task<IActionResult> Images(int sparePartId)
  {
    var spec = new SparePartImagesByIdSpec(sparePartId);
    var sparePart = await _sparePartRepository.FirstOrDefaultAsync(spec);
    List<SparePartImageViewModel> response = new List<SparePartImageViewModel>();

    foreach (var image in sparePart.SparePartImages)
    {
      response.Add(new SparePartImageViewModel()
      {
        FileName = image.Path
      });
    }
    return View(response);
  }
}
