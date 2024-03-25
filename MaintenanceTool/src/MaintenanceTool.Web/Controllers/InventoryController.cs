using MaintenanceTool.Core.InventoryAggregate;
using MaintenanceTool.Core.InventoryAggregate.Specifications;
using MaintenanceTool.SharedKernel.Interfaces;
using MaintenanceTool.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MaintenanceTool.Web.Controllers;

[Route("[controller]")]
public class InventoryController : Controller
{
  private readonly IRepository<Inventory> _repository;

  public InventoryController(IRepository<Inventory> repository)
  {
    _repository = repository;
  }

  [HttpPost]
  [Authorize(Roles = "Admin, User")]
  public async Task<IActionResult> AddStock(int sparePartId, string quantity, int companyId)
  {
    quantity = quantity.Replace(".", ",");
    decimal qty = Convert.ToDecimal(quantity);
    await _repository.AddAsync(new Inventory(DateTime.Now, @User.Identity.Name, qty, sparePartId));
    return RedirectToAction("Index", "SparePart", new { companyId = companyId });
  }

  [HttpGet("/Stock")]
  public async Task<IActionResult> Stock()
  {
    var inventoryList = (await _repository.ListAsync(new InventoryListWithSparePartSpec()))
      .GroupBy(grp =>new {SparePart = grp.SparePart})
      .Select(s=>new InventoryListViewModel()
    {
        SparePart = s.Key.SparePart.DisplayedName,
        Balance = s.Sum(x=>x.Qty)
    })
      .ToList();

    return View(inventoryList);
  }
}
