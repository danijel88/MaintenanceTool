using System;
using MaintenanceTool.Infrastructure.Data;
using MaintenanceTool.SharedKernel.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using MaintenanceTool.SharedKernel;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

#nullable disable

namespace MaintenanceTool.Infrastructure.Migrations
{
  public partial class SeedUserRoles : Migration
  {
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      var configuration = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json")
        .Build();

      var connectionString = configuration.GetConnectionString("DefaultConnection");
      var serviceProvider = new ServiceCollection()
        .AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString))
        .AddIdentityCore<IdentityUser>()
        .AddRoles<IdentityRole>()
        .AddEntityFrameworkStores<AppDbContext>()
        .Services
        .AddScoped<IDomainEventDispatcher, DomainEventDispatcher>()
        .AddScoped<IMediator,Mediator>()
        .AddTransient<ServiceFactory>(context =>
        {
          var serviceProvider = context.GetRequiredService<IServiceProvider>();
          return serviceType => serviceProvider.GetRequiredService(serviceType);
        })
        .BuildServiceProvider();
      
      var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
      var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

      // Create roles
      var adminRole = new IdentityRole { Name = "Admin", NormalizedName = "ADMIN" };
      var userRole = new IdentityRole { Name = "User", NormalizedName = "USER" };

      roleManager.CreateAsync(adminRole).GetAwaiter().GetResult();
      roleManager.CreateAsync(userRole).GetAwaiter().GetResult();

      // Create admin user
      var adminUser = new IdentityUser() { UserName = SeedCredentials.UserName, Email = SeedCredentials.UserName };
      var password = SeedCredentials.Password;
      var hashedPassword = userManager.PasswordHasher.HashPassword(adminUser, password);

      adminUser.PasswordHash = hashedPassword;
      adminUser.SecurityStamp = Guid.NewGuid().ToString(); // Generate a security stamp
      adminUser.ConcurrencyStamp = Guid.NewGuid().ToString(); // Generate a concurrency stamp

      userManager.CreateAsync(adminUser).GetAwaiter().GetResult();

      // Assign admin role to admin user
      userManager.AddToRoleAsync(adminUser, "Admin").GetAwaiter().GetResult();
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
      var serviceProvider = new ServiceCollection().AddIdentityCore<AppDbContext>().Services.BuildServiceProvider();
      var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
      var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
      var user = userManager.FindByEmailAsync(SeedCredentials.UserName).GetAwaiter().GetResult();
      userManager.DeleteAsync(user).GetAwaiter().GetResult();

      var adminRole = roleManager.FindByNameAsync("Admin").GetAwaiter().GetResult();
      roleManager.DeleteAsync(adminRole).GetAwaiter().GetResult();
      var userRole = roleManager.FindByNameAsync("User").GetAwaiter().GetResult();
      roleManager.DeleteAsync(userRole).GetAwaiter().GetResult();


    }
  }
}
