using System;
using System.Collections.Generic;

namespace MVC2.Data2;

public partial class Product
{
    public int Id { get; set; }

    public int Catalog { get; set; }

    public string? Name { get; set; }

    public string? Image { get; set; }

    public bool? Status { get; set; }

    public int? Price { get; set; }

    public string? Describe { get; set; }

    public virtual Catalog CatalogNavigation { get; set; } = null!;

    public virtual ICollection<ChitietBill> ChitietBills { get; set; } = new List<ChitietBill>();
}
