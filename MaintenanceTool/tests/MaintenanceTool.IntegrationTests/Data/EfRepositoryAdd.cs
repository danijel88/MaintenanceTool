using MaintenanceTool.Core.CompanyAggregate;
using MaintenanceTool.Core.ProjectAggregate;
using Xunit;

namespace MaintenanceTool.IntegrationTests.Data;
public class EfRepositoryAdd : BaseEfRepoTestFixture
{
  [Fact]
  public async Task AddsProjectAndSetsId()
  {
    var testProjectName = "testProject";
    var testProjectStatus = PriorityStatus.Backlog;
    var repository = GetRepository();
    var project = new Project(testProjectName, testProjectStatus);

    await repository.AddAsync(project);

    var newProject = (await repository.ListAsync())
                    .FirstOrDefault();

    Assert.Equal(testProjectName, newProject?.Name);
    Assert.Equal(testProjectStatus, newProject?.Priority);
    Assert.True(newProject?.Id > 0);
  }

  [Fact]
  public async Task AddCompanyAndSetsId()
  {
    var testCompany = "Test Company";
    var repository = GetCompanyRepository();
    var company = new Company(testCompany);

    await repository.AddAsync(company);

    var newCompany = (await repository.ListAsync())
      .FirstOrDefault();
    Assert.Equal(testCompany,newCompany.CompanyName);
    Assert.True(newCompany?.Id > 0);
  }
}
