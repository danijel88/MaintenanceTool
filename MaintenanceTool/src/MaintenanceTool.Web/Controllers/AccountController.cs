using MaintenanceTool.Web.Extensions;
using MaintenanceTool.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

namespace MaintenanceTool.Web.Controllers;

[Route("[controller]")]

public class AccountController : Controller
{
  private readonly UserManager<IdentityUser> _userManager;
  private readonly SignInManager<IdentityUser> _signInManager;

  public AccountController(UserManager<IdentityUser> userManager,SignInManager<IdentityUser> signInManager)
  {
    _userManager = userManager;
    _signInManager = signInManager;
  }

  [Authorize(Roles = "Admin")]
  [HttpGet("/Index")]
  public async Task<IActionResult> Index()
  {
    var users = await _userManager.Users.ToListAsync();
    List<UserViewModel> response = new List<UserViewModel>();
    foreach (var user in users)
    {
      var roles = await _userManager.GetRolesAsync(user);
      response.Add(new UserViewModel
      {
        Email = user.Email,
        Id = user.Id,
        Roles = roles.ToList()
      });
    }
    return View(response);
  }

  [Authorize(Roles = "Admin")]
  [HttpGet("/Create")]
  public IActionResult Create()
  {
    return View();
  }

  [Authorize(Roles = "Admin")]
  [HttpPost("/Create")]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> Create(CreateUserViewModel request)
  {
    if (!ModelState.IsValid)
    {
      ModelState.AddModelError("Error","Something went wrong. Please contact IT.");
      return View(request);
    }

    var user = new IdentityUser()
    {
      UserName = request.Email,
      Email = request.Email,
      SecurityStamp = Guid.NewGuid().ToString(),
      ConcurrencyStamp = Guid.NewGuid().ToString()
    };

    var hashedPassword =  _userManager.PasswordHasher.HashPassword(user, request.Password);
    user.PasswordHash = hashedPassword;

    var result = await _userManager.CreateAsync(user);
    if (!result.Succeeded)
    {
      ModelState.AddModelError("Error", result.Errors.FirstOrDefault().Description);
      return View(request);
    }

    var roleResult = await _userManager.AddToRoleAsync(user, "User");
    if (!roleResult.Succeeded)
    {
      ModelState.AddModelError("Error", roleResult.Errors.FirstOrDefault().Description);
    }

    return RedirectToAction();
  }

  [AllowAnonymous]
  [HttpGet("/Login")]
  public IActionResult Login()
  {
    return View();
  }

  [AllowAnonymous]
  [HttpPost("/Login")]
  public async Task<IActionResult> Login(LoginViewModel request)
  {
    var user = await _userManager.FindByEmailAsync(request.Email);
    var canSignIn = await _signInManager.CanSignInAsync(user);
    var result = await _signInManager.PasswordSignInAsync(request.Email, request.Password, false, false);
    if (!result.Succeeded)
    {
      TempData["alerts"] = this.ViewAlert(AlertType.Error, "Login failed. Please check credentials.");
      return View();
    }

    return RedirectToAction("Index", "Home");
  }

  [HttpGet]
  public async Task<IActionResult> LogOut()
  {
    await _signInManager.SignOutAsync();
    return RedirectToAction("Index", "Home");
  }
}
