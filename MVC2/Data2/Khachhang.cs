using System;
using System.Collections.Generic;

namespace MVC2.Data2;

public partial class Khachhang
{
    public int Id { get; set; }

    public string? Fullname { get; set; }

    public string? Email { get; set; }

    public int? Phone { get; set; }

    public string? Pwd { get; set; }

    public string? Position { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
