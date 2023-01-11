namespace Net.Advanced.Web.Endpoints;

public abstract class BaseRequestWithPagination
{
  public int PerPage { get; set; } = 100;

  public int Page { get; set; } = 1;
}
