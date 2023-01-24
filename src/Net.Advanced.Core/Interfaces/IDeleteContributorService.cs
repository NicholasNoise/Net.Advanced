using Ardalis.Result;

namespace Net.Advanced.Core.Interfaces;

public interface IDeleteContributorService
{
  public Task<Result> DeleteContributor(int contributorId);
}
