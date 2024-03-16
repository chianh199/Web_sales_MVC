using System;
using System.Collections.Generic;

namespace MVC2.Data;

public partial class ChitietBill
{
    public int? Bill { get; set; }

    public int? Product { get; set; }

    public int? Quantity { get; set; }

    public int? Total { get; set; }

    public virtual Order? BillNavigation { get; set; }

    public virtual Product? ProductNavigation { get; set; }
}
