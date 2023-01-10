using System.ComponentModel.DataAnnotations;

namespace Net.Advanced.Web.Endpoints.CategoryEndpoints;

public class CreateCategoryRequest
{
  public const string Route = "/Categories";

  [Required]
  public string? Name { get; set; }

  public string? Image { get; set; }

  public int? ParentId { get; set; }
}
