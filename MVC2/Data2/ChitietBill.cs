using System;
using System.Collections.Generic;

namespace MVC2.Data2;

public partial class ChitietBill
{
    public int Bill { get; set; }

    public int Product { get; set; }

    public int? Quantity { get; set; }

    public int? Total { get; set; }

    public int? Price { get; set; }

    public virtual Order BillNavigation { get; set; } = null!;

    public virtual Product ProductNavigation { get; set; } = null!;
}
