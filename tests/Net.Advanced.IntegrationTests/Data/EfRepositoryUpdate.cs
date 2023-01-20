using Net.Advanced.Core.ProjectAggregate;
using Microsoft.EntityFrameworkCore;
using Net.Advanced.Core.CatalogAggregate;
using Xunit;

namespace Net.Advanced.IntegrationTests.Data;

public class EfRepositoryUpdate : BaseEfRepoTestFixture
{
  protected override string DbName { get; } = nameof(EfRepositoryUpdate);

  [Fact]
  public async Task UpdatesProjectAfterAddingIt()
  {
    // add a project
    var repository = GetRepository<Project>();
    var initialName = Guid.NewGuid().ToString();
    var project = new Project(initialName, PriorityStatus.Backlog);

    await repository.AddAsync(project);

    // detach the item so we get a different instance
    _dbContext.Entry(project).State = EntityState.Detached;

    // fetch the item and update its title
    var newProject = (await repository.ListAsync())
        .FirstOrDefault(p => p.Name == initialName);
    Assert.NotNull(newProject);
    Assert.NotSame(project, newProject);
    var newName = Guid.NewGuid().ToString();
    newProject.UpdateName(newName);

    // Update the item
    await repository.UpdateAsync(newProject);

    // Fetch the updated item
    var updatedItem = (await repository.ListAsync())
        .FirstOrDefault(p => p.Name == newName);

    Assert.NotNull(updatedItem);
    Assert.NotEqual(project.Name, updatedItem.Name);
    Assert.Equal(project.Priority, updatedItem.Priority);
    Assert.Equal(newProject.Id, updatedItem.Id);
  }

  [Fact]
  public async Task UpdatesCategoryAfterAddingIt()
  {
    // Arrange
    var repository = GetRepository<Category>();
    var initialName = Guid.NewGuid().ToString();
    var category = new Category(initialName);

    await repository.AddAsync(category);

    // detach the item so we get a different instance
    _dbContext.Entry(category).State = EntityState.Detached;

    // fetch the item and update its title
    var newCategory = (await repository.ListAsync())
      .FirstOrDefault(c => c.Name == initialName);
    Assert.NotNull(newCategory);
    Assert.NotSame(category, newCategory);
    var newName = Guid.NewGuid().ToString();
    newCategory.UpdateName(newName);

    // Update the item
    await repository.UpdateAsync(newCategory);

    // Fetch the updated item
    var updatedItem = (await repository.ListAsync())
      .FirstOrDefault(c => c.Name == newName);

    // Assert.
    Assert.NotNull(updatedItem);
    Assert.NotEqual(category.Name, updatedItem.Name);
    Assert.Equal(newCategory.Id, updatedItem.Id);
  }

  [Fact]
  public async Task UpdatesParentCategory()
  {
    // Arrange
    var repository = GetRepository<Category>();
    var name = Guid.NewGuid().ToString();
    var category = new Category(name);
    var parentName = Guid.NewGuid().ToString();
    var parentCategory = new Category(parentName);

    await repository.AddAsync(parentCategory);
    await repository.AddAsync(category);

    // detach the item so we get a different instance
    _dbContext.Entry(category).State = EntityState.Detached;

    // fetch the item and update its title
    var categories = await repository.ListAsync();
    Assert.Equal(2, categories.Count);

    var newCategory = categories
      .FirstOrDefault(c => c.Name == name);
    Assert.NotNull(newCategory);
    Assert.NotSame(category, newCategory);

    newCategory.UpdateParent(parentCategory);

    // Update the item
    await repository.UpdateAsync(newCategory);

    // Fetch the updated item
    var updatedItem = (await repository.ListAsync())
      .FirstOrDefault(c => c.Parent is not null);

    // Assert.
    Assert.NotNull(updatedItem);
    Assert.Equal(newCategory.Id, updatedItem.Id);
    Assert.Equal(newCategory.Parent, updatedItem.Parent);
  }

  [Fact]
  public async Task UpdatesProductAfterAddingIt()
  {
    // Arrange
    const string testCategoryName = "testCategory";
    const string initialName = "testProduct";
    var repository = GetRepository<Product>();
    var category = new Category(testCategoryName);
    var product = new Product(initialName, 1m, 1) { Category = category };

    await repository.AddAsync(product);

    // detach the item so we get a different instance
    _dbContext.Entry(product).State = EntityState.Detached;

    // fetch the item and update its title
    var newProduct = (await repository.ListAsync())
      .FirstOrDefault(c => c.Name == initialName);
    Assert.NotNull(newProduct);
    Assert.NotSame(product, newProduct);
    newProduct.Amount = 1000;

    // Update the item
    await repository.UpdateAsync(newProduct);

    // Fetch the updated item
    var updatedItem = (await repository.ListAsync())
      .FirstOrDefault(c => c.Name == initialName);

    // Assert.
    Assert.NotNull(updatedItem);
    Assert.Equal(newProduct.Id, updatedItem.Id);
    Assert.NotEqual(product.Amount, updatedItem.Amount);
  }
}
