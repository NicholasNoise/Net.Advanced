namespace Net.Advanced.Web.Endpoints.ProductEndpoints;

public class GetProductByIdRequest
{
  public const string Route = "/Products/{ProductId:int}";
  public static string BuildRoute(int productId) => Route.Replace("{ProductId:int}", productId.ToString());

  public int ProductId { get; set; }
}
