using System;
using System.Collections.Generic;

namespace MVC2.Data2;

public partial class Order
{
    public int Id { get; set; }

    public int? Total { get; set; }

    public int? Khachhang { get; set; }

    public int? NvCreate { get; set; }

    public int? NvShip { get; set; }

    public string? Status { get; set; }

    public DateTime? DateCreate { get; set; }

    public string? Fullname { get; set; }

    public int? Phone { get; set; }

    public string? Email { get; set; }

    public string? Position { get; set; }

    public virtual ICollection<ChitietBill> ChitietBills { get; set; } = new List<ChitietBill>();

    public virtual Khachhang? KhachhangNavigation { get; set; }

    public virtual Nhanvien? NvCreateNavigation { get; set; }

    public virtual Nhanvien? NvShipNavigation { get; set; }
}
