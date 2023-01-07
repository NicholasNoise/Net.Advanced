using Net.Advanced.SharedKernel;

namespace Net.Advanced.Core.CartAggregate;
public class Category : EntityBase
{
  public string Name { get; set; } = string.Empty;
  public string Image { get; set; } = string.Empty;
  public int Parent { get; set; }
}
