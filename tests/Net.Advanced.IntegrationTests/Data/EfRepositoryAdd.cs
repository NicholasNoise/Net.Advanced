using Microsoft.EntityFrameworkCore;
using Net.Advanced.Core.CatalogAggregate;
using Net.Advanced.Core.ProjectAggregate;
using Xunit;

namespace Net.Advanced.IntegrationTests.Data;

public class EfRepositoryAdd : BaseEfRepoTestFixture
{
  protected override string DbName { get; } = nameof(EfRepositoryAdd);

  [Fact]
  public async Task AddsProjectAndSetsId()
  {
    var testProjectName = "testProject";
    var testProjectStatus = PriorityStatus.Backlog;
    var repository = GetRepository<Project>();
    var project = new Project(testProjectName, testProjectStatus);

    await repository.AddAsync(project);

    var newProject = (await repository.ListAsync())
                    .FirstOrDefault();

    Assert.Equal(testProjectName, newProject?.Name);
    Assert.Equal(testProjectStatus, newProject?.Priority);
    Assert.True(newProject?.Id > 0);
  }

  [Fact]
  public async Task AddsCategory()
  {
    // Arrange.
    const string testCategoryName = "testCategory";
    var repository = GetRepository<Category>();
    var category = new Category(testCategoryName);

    // Act.
    await repository.AddAsync(category);

    // detach the item so we get a different instance
    _dbContext.Entry(category).State = EntityState.Detached;

    var newCategory = (await repository.ListAsync())
      .FirstOrDefault();

    // Assert.
    Assert.NotNull(newCategory);
    Assert.Equal(testCategoryName, newCategory.Name);
    Assert.True(newCategory.Id > 0);
  }
}
