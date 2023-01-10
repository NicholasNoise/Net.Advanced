using Net.Advanced.Core.CatalogAggregate;
using Net.Advanced.Core.ProjectAggregate;
using Xunit;

namespace Net.Advanced.IntegrationTests.Data;

public class EfRepositoryDelete : BaseEfRepoTestFixture
{
  protected override string DbName { get; } = nameof(EfRepositoryDelete);

  [Fact]
  public async Task DeletesProjectAfterAddingIt()
  {
    // add a project
    var repository = GetRepository<Project>();
    var initialName = Guid.NewGuid().ToString();
    var project = new Project(initialName, PriorityStatus.Backlog);
    await repository.AddAsync(project);

    // delete the item
    await repository.DeleteAsync(project);

    // verify it's no longer there
    Assert.DoesNotContain(await repository.ListAsync(),
        p => p.Name == initialName);
  }

  [Fact]
  public async Task DeletesCategoryAfterAddingIt()
  {
    // Arrange.
    var repository = GetRepository<Category>();
    var initialName = Guid.NewGuid().ToString();
    var category = new Category(initialName);
    await repository.AddAsync(category);

    // Act.
    await repository.DeleteAsync(category);

    // Assert.
    Assert.DoesNotContain(await repository.ListAsync(),
      c => c.Name == initialName);
  }

  [Fact]
  public async Task DeletesProductAfterAddingIt()
  {
    // Arrange.
    var repository = GetRepository<Product>();
    var initialName = Guid.NewGuid().ToString();
    var product = new Product(initialName, 1m);
    await repository.AddAsync(product);

    // Act.
    await repository.DeleteAsync(product);

    // Assert.
    Assert.DoesNotContain(await repository.ListAsync(),
      c => c.Name == initialName);
  }
}
