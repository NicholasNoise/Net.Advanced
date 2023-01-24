using System.Linq.Expressions;
using Ardalis.Specification;

namespace Net.Advanced.SharedKernel.Interfaces;

// from Ardalis.Specification
public interface IRepository<T> : IRepositoryBase<T>
  where T : class, IAggregateRoot
{
  Task<IReadOnlyList<T>> ListAsync(int perPage, int page, Expression<Func<T, bool>>? filter, CancellationToken cancellationToken);
}
