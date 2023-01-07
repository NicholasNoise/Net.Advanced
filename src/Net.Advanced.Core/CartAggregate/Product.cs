using Net.Advanced.SharedKernel;

namespace Net.Advanced.Core.CartAggregate;
public class Product : EntityBase
{
  public string Name { get; set; } = string.Empty;
  public string Description { get; set; } = string.Empty;
  public string Image { get; set; } = string.Empty;
  public int CategoryId { get; set; }
  public decimal Price { get; set; }
  public uint Amount { get; set; }
}
