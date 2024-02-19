using MaintenanceTool.Core.ProductAggregate;
using MaintenanceTool.Core.ProductAggregate.Specifications;
using MaintenanceTool.SharedKernel.Interfaces;
using MaintenanceTool.Web.Extensions;
using MaintenanceTool.Web.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace MaintenanceTool.Web.Controllers;

[Route("[controller]")]
public class ProductController : Controller
{
  private readonly IRepository<Product> _productRepository;
  private readonly IWebHostEnvironment _webHostEnvironment;

  public ProductController(IRepository<Product> productRepository, IWebHostEnvironment webHostEnvironment)
  {
    _productRepository = productRepository;
    _webHostEnvironment = webHostEnvironment;
  }

  [HttpGet("{companyId}")]
  public async Task<IActionResult> Index(int companyId)
  {
    ViewBag.CompanyId = companyId;
    var productSpecification = new ProductsByCompanyIdSpec(companyId);
    var products = (await _productRepository.ListAsync(productSpecification))
      .Select(product => new ProductViewModel(
        product.Description,
        product.Model,
        product.ProductName,
        product.SerialNumber,
        product.Id))
      .ToList();
    return View(products);
  }

  [HttpGet("{companyId}/Create")]
  public IActionResult Create(int companyId)
  {
    return View();
  }

  [HttpPost("{companyId}/Create")]
  public async Task<IActionResult> Create(int companyId, CreateProductViewModel request)
  {
    if (!ModelState.IsValid)
    {
      return View();
    }

    await _productRepository.AddAsync(new Product(companyId, request.Description, request.Model, request.ProductName,
      request.SerialNumber,request.EachMonths,request.LastService));
    var saved = await _productRepository.SaveChangesAsync();
    if (saved == 0)
    {
      TempData["alerts"] = this.ViewAlert(AlertType.Success, "Product has been created.");
    }
    else
    {
      return View();
    }

    return RedirectToAction("Index", new { companyId });
  }

  [HttpGet("{productId}/Upload")]
  public IActionResult Upload(int productId)
  {
    return View();
  }
  [HttpPost("{productId}/Upload")]
  public async Task<IActionResult> Upload(int productId, IFormFile file)
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

        var product = await _productRepository.GetByIdAsync(productId);
        if (product == null)
        {
          ViewBag.Message = "Product not found.";
          return View("Upload");
        }
        else
        {
          product.UploadImage(new ProductImage(productId, relativePath));
          await _productRepository.UpdateAsync(product);
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

  [HttpGet("{productId}/Images")]
  public async Task<IActionResult> Images(int productId)
  {
    var spec = new ProductImagesByProductIdSpec(productId);
    var product = await _productRepository.FirstOrDefaultAsync(spec);
    List<ProductImageViewModel> response = new List<ProductImageViewModel>();

    foreach (var image in product.ProductImages)
    {
      response.Add(new ProductImageViewModel
      {
        FileName = image.Path
      });
    }
    return View(response);
  }

  [HttpGet("/NextServices")]
  public async Task<IActionResult> NextServices()
  {
    var product = (await _productRepository.ListAsync()).Select(s => new ProductsNextServiceViewModel
      {
        LastService = s.LastService, NextService = s.NextService, Name = s.DisplayedName
      })
      .ToList();
    return View(product);
  }
}
