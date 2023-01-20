using System.Linq.Expressions;
using Ardalis.Specification.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Net.Advanced.SharedKernel.Interfaces;

namespace Net.Advanced.Infrastructure.Data;

// inherit from Ardalis.Specification type
public class EfRepository<T> : RepositoryBase<T>, IReadRepository<T>, IRepository<T> where T : class, IAggregateRoot
{
  private readonly DbContext _dbContext;

  public EfRepository(AppDbContext dbContext) : base(dbContext)
  {
    _dbContext = dbContext;
  }

  public async Task<IReadOnlyList<T>> ListAsync(
    int perPage,
    int page,
    Expression<Func<T, bool>>? filter,
    CancellationToken cancellationToken)
  {
    return await _dbContext.Set<T>().Where(filter ?? (t => true)).Skip(perPage * (page - 1)).Take(perPage).ToListAsync(cancellationToken);
  }
}
