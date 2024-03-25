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
public class GroupController : Controller
{
  private readonly IRepository<Group> _groupRepository;
  private readonly IRepository<Product> _productRepository;
  private readonly IRepository<SparePart> _sparePartRepository;

  public GroupController(IRepository<Group> groupRepository
    , IRepository<SparePart> sparePartRepository
    , IRepository<Product> productRepository)
  {
    _groupRepository = groupRepository;
    _sparePartRepository = sparePartRepository;
    _productRepository = productRepository;
  }

  [Authorize(Roles = "Admin,User")]
  [HttpGet("{plantId}/Create")]
  public async Task<IActionResult> Create(int plantId)
  {
    ViewBag.PlantId = plantId;
    ViewBag.SpareParts = new SelectList(await _sparePartRepository.ListAsync(), "Id", "DisplayedName");
    ViewBag.Products = new SelectList(await _productRepository.ListAsync(), "Id", "DisplayedName");
    var response = new CreateGroupViewModel();
    return View(response);
  }
  [Authorize(Roles = "Admin,User")]
  [HttpPost("{plantId}/Create")]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> Create(int plantId, [FromBody] CreateGroupViewModel request)
  {
    if (!ModelState.IsValid)
    {
      ViewBag.SpareParts = new SelectList(await _sparePartRepository.ListAsync(), "Id", "DisplayedName");
      ViewBag.Products = new SelectList(await _productRepository.ListAsync(), "Id", "DisplayedName");
      var response = new CreateGroupViewModel();
      return View(response);
    }

    var newGroup = await _groupRepository.AddAsync(new Group(request.GroupName, plantId));
    foreach (var groupConfig in request.GroupConfigs)
    {
      newGroup.AddGroupConfig(new GroupConfig(newGroup.Id, groupConfig.ProductId, groupConfig.SparePartId));
    }

    await _groupRepository.UpdateAsync(newGroup);

    return Ok(Url.Action("Index", "Home"));
  }

  [HttpGet("Details/{id}")]
  public async Task<IActionResult> Details(int id)
  {
    var spec = new GroupByIdWithGroupConfigsDetailsSpec(id);
    var group = await _groupRepository.FirstOrDefaultAsync(spec);
    ViewBag.PlantId = group.PlantId;
    ViewBag.GroupId = id;
    var response = group
      .GroupConfigs
      .GroupBy(grp => new
      {
        Product = grp.Product.DisplayedName, Spare = grp.SparePart.DisplayedName, GroupConfigId = grp.Id
      })
      .Select(s => new GroupDetailsViewModel(s.Key.GroupConfigId, s.Key.Product, s.Key.Spare))
      .ToList();

    return View(response);
  }
  [Authorize(Roles = "Admin,User")]
  [HttpPost("{groupId}/Delete/{id}")]
  public async Task<IActionResult> Delete(int id,int groupId)
  {
    var group = await _groupRepository.FirstOrDefaultAsync(new GroupConfigByIdSpec(groupId));
    var groupConfig = group.GetById(id);
    group.RemoveGroupConfig(groupConfig);
    await _groupRepository.UpdateAsync(group);
    await _groupRepository.SaveChangesAsync();
    return Ok();
  }

  [HttpGet("ServiceLog/{id}")]
  public async Task<IActionResult> ServiceLog(int id)
  {
    var spec = new ServiceLogsByGroupIdSpec(id);
    var group = await _groupRepository.FirstOrDefaultAsync(spec);
    var response = group
      .ServiceLogs
      .Select(s => new ServiceLogViewModel
      {
        CreatedDate = s.CreatedDate, Description = s.Description, DoneBy = s.DoneBy
      }).ToList();
    return View(response);
  }
  [Authorize(Roles = "Admin,User")]
  [HttpGet("Edit/{plantId}/{groupId}")]
  public async Task<IActionResult> Edit(int plantId, int groupId)
  {
    ViewBag.PlantId = plantId;
    ViewBag.GroupId = groupId;
    var spec = new GroupByIdWithGroupConfigsDetailsSpec(groupId);
    var group = await _groupRepository.FirstOrDefaultAsync(spec);
    ViewBag.SpareParts = new SelectList(await _sparePartRepository.ListAsync(), "Id", "DisplayedName");
    ViewBag.Products = new SelectList(await _productRepository.ListAsync(), "Id", "DisplayedName");
    var response = group
      .GroupConfigs
      .GroupBy(grp => new
      {
        Product = grp.Product.DisplayedName,
        Spare = grp.SparePart.DisplayedName,
        GroupConfigId = grp.Id
      })
      .Select(s => new GroupDetailsViewModel(s.Key.GroupConfigId, s.Key.Product, s.Key.Spare))
      .ToList();
    EditGroupConfigViewModel result = new EditGroupConfigViewModel();
    result.Response = response;
    return View(result);
  }

  [Authorize(Roles = "Admin,User")]
  [HttpPost("Edit/{plantId}/{groupId}")]
  public async Task<IActionResult> Edit(int plantId, int groupId,[FromBody] EditGroupConfigViewModel request)
  {
    if (!ModelState.IsValid)
    {
      ViewBag.PlantId = plantId;
      ViewBag.GroupId = groupId;
      ViewBag.SpareParts = new SelectList(await _sparePartRepository.ListAsync(), "Id", "DisplayedName");
      ViewBag.Products = new SelectList(await _productRepository.ListAsync(), "Id", "DisplayedName");
      var spec = new GroupByIdWithGroupConfigsDetailsSpec(groupId);
      var group = await _groupRepository.FirstOrDefaultAsync(spec);
      var response = group
        .GroupConfigs
        .GroupBy(grp => new
        {
          Product = grp.Product.DisplayedName,
          Spare = grp.SparePart.DisplayedName,
          GroupConfigId = grp.Id
        })
        .Select(s => new GroupDetailsViewModel(s.Key.GroupConfigId, s.Key.Product, s.Key.Spare))
        .ToList();
      EditGroupConfigViewModel result = new EditGroupConfigViewModel();
      result.Response = response;
      return View(result);
    }

    var newGroup = await _groupRepository.GetByIdAsync(groupId);
    foreach (var groupConfig in request.GroupConfigs)
    {
      newGroup.AddGroupConfig(new GroupConfig(newGroup.Id, groupConfig.ProductId, groupConfig.SparePartId));
    }

    await _groupRepository.UpdateAsync(newGroup);
    return Ok(Url.Action("Index", "Home"));
  }
}
