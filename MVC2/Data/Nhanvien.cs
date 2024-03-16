using System;
using System.Collections.Generic;

namespace MVC2.Data;

public partial class Nhanvien
{
    public int Id { get; set; }

    public int Role { get; set; }

    public string? Fullname { get; set; }

    public int? Phone { get; set; }

    public string? Pwd { get; set; }

    public virtual ICollection<Order> OrderNvCreateNavigations { get; set; } = new List<Order>();

    public virtual ICollection<Order> OrderNvShipNavigations { get; set; } = new List<Order>();
}
