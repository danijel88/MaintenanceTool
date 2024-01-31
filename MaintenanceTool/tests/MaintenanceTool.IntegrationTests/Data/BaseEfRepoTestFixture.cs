using MaintenanceTool.Core.CompanyAggregate;
using MaintenanceTool.Core.ProjectAggregate;
using MaintenanceTool.Infrastructure.Data;
using MaintenanceTool.SharedKernel.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace MaintenanceTool.IntegrationTests.Data;
public abstract class BaseEfRepoTestFixture
{
  protected AppDbContext _dbContext;

  protected BaseEfRepoTestFixture()
  {
    var options = CreateNewContextOptions();
    var mockEventDispatcher = new Mock<IDomainEventDispatcher>();

    _dbContext = new AppDbContext(options, mockEventDispatcher.Object);
  }

  protected static DbContextOptions<AppDbContext> CreateNewContextOptions()
  {
    // Create a fresh service provider, and therefore a fresh
    // InMemory database instance.
    var serviceProvider = new ServiceCollection()
        .AddEntityFrameworkInMemoryDatabase()
        .BuildServiceProvider();

    // Create a new options instance telling the context to use an
    // InMemory database and the new service provider.
    var builder = new DbContextOptionsBuilder<AppDbContext>();
    builder.UseInMemoryDatabase("cleanarchitecture")
           .UseInternalServiceProvider(serviceProvider);

    return builder.Options;
  }

  protected EfRepository<Project> GetRepository()
  {
    return new EfRepository<Project>(_dbContext);
  }

  protected EfRepository<Company> GetCompanyRepository()
  {
    return new EfRepository<Company>(_dbContext);
  }
}
