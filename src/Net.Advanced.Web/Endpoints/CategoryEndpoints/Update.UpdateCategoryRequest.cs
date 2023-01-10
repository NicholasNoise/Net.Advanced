using System.ComponentModel.DataAnnotations;

namespace Net.Advanced.Web.Endpoints.CategoryEndpoints;

public class UpdateCategoryRequest
{
  public const string Route = "/Categories";
  [Required]
  public int Id { get; set; }

  [Required]
  public string? Name { get; set; }

  public string? Image { get; set; }

  public int? ParentId { get; set; }
}
