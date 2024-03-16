using System;
using System.Collections.Generic;

namespace MVC2.Data;

public partial class Order
{
    public int Id { get; set; }

    public int? Total { get; set; }

    public int? Khachhang { get; set; }

    public int? NvCreate { get; set; }

    public int? NvShip { get; set; }

    public string? Status { get; set; }

    public DateTime? DateCreate { get; set; }

    public virtual Khachhang? KhachhangNavigation { get; set; }

    public virtual Nhanvien? NvCreateNavigation { get; set; }

    public virtual Nhanvien? NvShipNavigation { get; set; }
}
