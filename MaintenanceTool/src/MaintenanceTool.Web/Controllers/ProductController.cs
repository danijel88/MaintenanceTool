using MaintenanceTool.Core.ProductAggregate;
using MaintenanceTool.Core.ProductAggregate.Specifications;
using MaintenanceTool.SharedKernel.Interfaces;
using MaintenanceTool.Web.Extensions;
using MaintenanceTool.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace MaintenanceTool.Web.Controllers;

[Route("[controller]")]
public class ProductController : Controller
{
  private readonly IRepository<Product> _productRepository;

  public ProductController(IRepository<Product> productRepository)
  {
    _productRepository = productRepository;
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
        product.SerialNumber))
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
      request.SerialNumber));
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
}
