﻿using System.Xml.Linq;
using Ardalis.Result;
using MaintenanceTool.Core.Interfaces;
using MaintenanceTool.Core.InventoryAggregate;
using MaintenanceTool.Core.InventoryAggregate.Specifications;
using MaintenanceTool.Core.MaintenanceAggregate;
using MaintenanceTool.Core.MaintenanceAggregate.Specifications;
using MaintenanceTool.Core.ProductAggregate;
using MaintenanceTool.SharedKernel.Interfaces;
using MaintenanceTool.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MaintenanceTool.Web.Controllers;

[Route("[controller]")]

public class TransactionController : Controller
{
  private readonly IRepository<Group> _groupRepository;
  private readonly IGroupConfigSearchService _searchService;
  private readonly IRepository<Inventory> _inventoryRepository;
  private readonly IRepository<Product> _productRepository;
  private readonly IRepository<Transaction> _transactionRepository;


  public TransactionController(IRepository<Transaction> transactionRepository
    , IRepository<Group> groupRepository
    , IGroupConfigSearchService searchService
    , IRepository<Inventory> inventoryRepository
    ,IRepository<Product> productRepository)
  {
    _transactionRepository = transactionRepository;
    _groupRepository = groupRepository;
    _searchService = searchService;
    _inventoryRepository = inventoryRepository;
    _productRepository = productRepository;
  }

  [HttpGet("{plantId}/Create/{groupId}")]
  [Authorize(Roles = "Admin,User")]
  public async Task<IActionResult> Create(int groupId, int plantId)
  {
    var response = await InitializedCreate(groupId, plantId);
    return View(response);
  }

  [Authorize(Roles = "Admin,User")]
  [HttpPost("{plantId}/Create/{groupId}")]
  [AutoValidateAntiforgeryToken]
  public async Task<IActionResult> Create(int groupId, int plantId, [FromBody] CreateTransactionViewModel model)
  {
    if (model.Description == null)
    {
      model.Description = string.Empty;
    }
    if (string.IsNullOrEmpty(model.DoneBy))
    {
      ModelState.AddModelError("Error", "No data selected");
      var errorResponse = await InitializedCreate(groupId, plantId);
      return BadRequest(errorResponse);
    }
    
    var mt = (MaintenanceType)model.MaintenanceType;
    var request = new List<Transaction>();
    foreach (var element in model.DetailTransactions)
    {
      request.Add(new Transaction(model.CreatedDate, model.DoneBy, mt, plantId, element.GroupConfigId, -element.Quantity,
        TransactionType.Consume,model.Description));
      var group = await _groupRepository.FirstOrDefaultAsync(new GroupByIdWithGroupConfigsDetailsSpec(groupId));
      var config = group.GroupConfigs.FirstOrDefault(w => w.Id == element.GroupConfigId);
      await _inventoryRepository.AddAsync(new Inventory(DateTime.Now, User.Identity.Name, -element.Quantity, config.SparePartId));
      if (element.MoveService == "on" || element.MoveService == "true")
      {
        var product = await _productRepository.GetByIdAsync(config.ProductId);
        product.UpdateLastService(model.CreatedDate);
        await _productRepository.UpdateAsync(product);
      }
    }

    if (request.Count > 0)
    {
      var response = await _transactionRepository.AddRangeAsync(request);
    }

    if (model.DetailTransactions.Count == 0)
    {
      var group = await _groupRepository.GetByIdAsync(groupId);
      group.AddServiceLog(new ServiceLog(model.Description,model.DoneBy,model.CreatedDate));
      await _groupRepository.UpdateAsync(group);
    }
    
    return Ok(Url.Action("Index", "Home"));
  }

  [HttpGet("{groupId}/Spares/{productId}")]
  public async Task<JsonResult> GetSparesByProductId(int productId, int groupId)
  {
    var result = await _searchService.GetGroupConfigSearchAsync(groupId, productId);
    if (result.Status == ResultStatus.Ok)
    {
      var response = new List<GroupConfigViewModel>();
      response = result.Value.Select(
        gc => new GroupConfigViewModel { GroupConfigId = gc.Id, SparePartName = gc.SparePart.SparePartName }).ToList();
      return Json(response);
    }

    return new JsonResult(BadRequest(result.ValidationErrors));
  }

  [HttpGet("{groupId}/Details/{plantId}")]
  public async Task<IActionResult> Details(int groupId, int plantId)
  {
    var spec = new ListOfTransactionsByGroupIdSpec(groupId, plantId);
    var response = (await _transactionRepository.ListAsync(spec))
      .Select(s=> new TransactionDetailsViewModel(s.DoneBy,s.CreatedDate.Date,s.MaintenanceType.ToString("G"),s.GroupConfig.Product.DisplayedName,s.GroupConfig.SparePart.DisplayedName,s.Description))
      .ToList();
    return View(response);
  }

  private async Task<CreateTransactionViewModel> InitializedCreate(int groupId, int plantId)
  {
    var spec = new GroupByIdWithGroupConfigsDetailsSpec(groupId);
    var group = await _groupRepository.FirstOrDefaultAsync(spec);
    ViewBag.Products = new SelectList(group.GroupConfigs.DistinctBy(x => x.ProductId).ToList(), "ProductId",
      "Product.DisplayedName");
    ViewBag.GroupId = groupId;
    ViewBag.PlantId = plantId;
    var response = new CreateTransactionViewModel();
    return response;
  }

}
