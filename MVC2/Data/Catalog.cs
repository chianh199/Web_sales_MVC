using System;
using System.Collections.Generic;

namespace MVC2.Data;

public partial class Catalog
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
