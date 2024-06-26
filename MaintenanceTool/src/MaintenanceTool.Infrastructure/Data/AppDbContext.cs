﻿using System.Reflection;
using MaintenanceTool.Core.CompanyAggregate;
using MaintenanceTool.Core.InventoryAggregate;
using MaintenanceTool.Core.MaintenanceAggregate;
using MaintenanceTool.Core.ProductAggregate;
using MaintenanceTool.Core.ProjectAggregate;
using MaintenanceTool.SharedKernel;
using MaintenanceTool.SharedKernel.Interfaces;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MaintenanceTool.Infrastructure.Data;
public class AppDbContext : IdentityDbContext
{
  private readonly IDomainEventDispatcher? _dispatcher;

  public AppDbContext(DbContextOptions<AppDbContext> options,
    IDomainEventDispatcher? dispatcher)
      : base(options)
  {
    _dispatcher = dispatcher;
  }

  public DbSet<Company> Companies => Set<Company>();
  public DbSet<Plant> Plants => Set<Plant>();
  public DbSet<Product> Products => Set<Product>();
  public DbSet<SparePart> SpareParts => Set<SparePart>();
  public DbSet<Transaction> Transactions => Set<Transaction>();
  public DbSet<Group> Groups => Set<Group>();
  public DbSet<Inventory> Inventories => Set<Inventory>();
  public DbSet<ToDoItem> ToDoItems => Set<ToDoItem>();
  public DbSet<Project> Projects => Set<Project>();

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);
    modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
  }

  public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
  {
    int result = await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

    // ignore events if no dispatcher provided
    if (_dispatcher == null) return result;

    // dispatch events only if save was successful
    var entitiesWithEvents = ChangeTracker.Entries<EntityBase>()
        .Select(e => e.Entity)
        .Where(e => e.DomainEvents.Any())
        .ToArray();

    await _dispatcher.DispatchAndClearEvents(entitiesWithEvents);

    return result;
  }

  public override int SaveChanges()
  {
    return SaveChangesAsync().GetAwaiter().GetResult();
  }
}
