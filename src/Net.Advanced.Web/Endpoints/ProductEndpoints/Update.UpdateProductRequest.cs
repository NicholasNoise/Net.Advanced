using System.ComponentModel.DataAnnotations;

namespace Net.Advanced.Web.Endpoints.ProductEndpoints;

public class UpdateProductRequest
{
  public const string Route = "/Products";
  [Required]
  public int Id { get; set; }

  [Required]
  public string? Name { get; set; }

  public string? Description { get; set; }

  public string? Image { get; set; }

  [Required]
  public int CategoryId { get; set; }

  [Required]
  public decimal Price { get; set; }

  [Required]
  public uint Amount { get; set; }
}
