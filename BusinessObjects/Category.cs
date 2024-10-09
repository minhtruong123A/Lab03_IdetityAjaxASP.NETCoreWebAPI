﻿using System;
using System.Collections.Generic;

namespace BusinessObjects;

public partial class Category
{
    public int CategoryId { get; set; }

    public string CategoryName { get; set; } = string.Empty;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
