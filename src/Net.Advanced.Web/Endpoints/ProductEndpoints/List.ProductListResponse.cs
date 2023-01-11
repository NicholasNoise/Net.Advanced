namespace Net.Advanced.Web.Endpoints.ProductEndpoints;

public class ProductListResponse : BaseResponseWithPagination
{
  public ProductListResponse() : base()
  {
  }

  public ProductListResponse(int count, int page, int perPage, Func<int, int, string> buildRoute)
    : base(count, page, perPage, buildRoute)
  {
  }

  public List<ProductRecord> Products { get; set; } = new();
}
