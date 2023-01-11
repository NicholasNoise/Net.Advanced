namespace Net.Advanced.Web.Endpoints;

public class BaseResponseWithPagination
{
  protected BaseResponseWithPagination()
  {
  }

  protected BaseResponseWithPagination(int count, int page, int perPage, Func<int, int, string> buildRoute)
  {
    Count = count;
    Prev = page == 1 ? null : buildRoute(page - 1, perPage);
    Next = page * perPage >= count ? null : buildRoute(page + 1, perPage);
  }

  public int Count { get; set; }

  public string? Prev { get; set; }

  public string? Next { get; set; }
}
