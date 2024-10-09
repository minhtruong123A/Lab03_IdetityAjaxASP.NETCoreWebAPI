using System;
using System.Collections.Generic;

namespace BusinessObjects;

public partial class Product
{
    public int ProductId { get; set; }

    public string ProductName { get; set; } = string.Empty;

    public int? CategoryId { get; set; }

    public int UnitsInStock { get; set; }

    public decimal UnitPrice { get; set; }

    public virtual Category? Category { get; set; }
}
