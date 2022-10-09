using Core.Entities.Abstract;

namespace Entities.Concrete;

public class Product : IEntity
{
    public int ProductId { get; set; }
    public int CategoryId { get; set; }
    public string ProductName { get; set; } = default!;
    public string QuantityPerUnit { get; set; } = default!;
    public decimal UnitPrice { get; set; }
    public short UnitsInStock { get; set; }
}