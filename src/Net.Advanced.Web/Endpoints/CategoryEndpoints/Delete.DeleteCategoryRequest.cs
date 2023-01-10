namespace Net.Advanced.Web.Endpoints.CategoryEndpoints;

public class DeleteCategoryRequest
{
  public const string Route = "/Categories/{CategoryId:int}";
  public static string BuildRoute(int categoryId) => Route.Replace("{CategoryId:int}", categoryId.ToString());

  public int CategoryId { get; set; }
}
