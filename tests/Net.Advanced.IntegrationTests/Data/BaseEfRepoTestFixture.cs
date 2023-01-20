using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Net.Advanced.Infrastructure.Data;
using Net.Advanced.SharedKernel.Interfaces;

namespace Net.Advanced.IntegrationTests.Data;

public abstract class BaseEfRepoTestFixture
{
  protected AppDbContext _dbContext;

  protected BaseEfRepoTestFixture()
  {
    var options = CreateNewContextOptions();
    var mockEventDispatcher = new Mock<IDomainEventDispatcher>();

    _dbContext = new AppDbContext(options, mockEventDispatcher.Object);
    _dbContext.Database.EnsureDeleted();
    _dbContext.Database.EnsureCreated();
  }

  protected abstract string DbName { get; }

  protected DbContextOptions<AppDbContext> CreateNewContextOptions()
  {
    // Create a fresh service provider, and therefore a fresh
    // Postgres database instance.
    var config = AppSettings.InitConfiguration();
    var serviceProvider = new ServiceCollection()
        .AddEntityFrameworkNpgsql()
        .AddSingleton(config)
        .BuildServiceProvider();

    // Create a new options instance telling the context to use an
    // Postgres database and the new service provider.
    var connectionString = AppSettings.GetUniquePostgreSqlConnectionString(config, DbName);
    var builder = new DbContextOptionsBuilder<AppDbContext>();
    builder.UseNpgsql(connectionString)
           .UseInternalServiceProvider(serviceProvider);

    return builder.Options;
  }

  protected EfRepository<T> GetRepository<T>()
    where T : class, IAggregateRoot
  {
    return new EfRepository<T>(_dbContext);
  }
}
