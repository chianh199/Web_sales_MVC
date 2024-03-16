using Microsoft.AspNetCore.Mvc;
using MVC2.Data2;
using MVC2.ViewModels;
using MVC2.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
namespace MVC2.Controllers
{
	public class Cart : Controller
	{
        private readonly Food2Context db;

        public Cart(Food2Context context) => db = context;
        const string CART_KEY = "MYCART";
        public List<CartItem> Carts => HttpContext.Session.Get<List<CartItem>>(CART_KEY) ?? new List<CartItem>();

        public IActionResult Index()
        {
            return View(Carts);
        }
        public IActionResult AddToCart(int id, int quantity = 1)
        {
            var gioHang = Carts;
            var item = gioHang.SingleOrDefault(p => p.Id == id);
            if (item == null)
            {
                var hangHoa = db.Products.SingleOrDefault(p => p.Id == id);
                if (hangHoa == null)
                {
                    TempData["Message"] = $"Không tìm thấy hàng hóa có mã {id}";
                    return Redirect("/404");
                }
                item = new CartItem
                {
                    Id = hangHoa.Id,
                    Name = hangHoa.Name,
                    Price = hangHoa.Price ?? 0,
                    Image = hangHoa.Image ?? "",
                    SoLuong = quantity
                };
                gioHang.Add(item);
            }
            else
            {
                item.SoLuong += quantity;
            }

            HttpContext.Session.Set(CART_KEY, gioHang);

            return RedirectToAction("Index");
        }
        public IActionResult RemoveCart(int id)
        {
            var gioHang = Carts;
            var item = gioHang.SingleOrDefault(p => p.Id == id);
            if (item != null)
            {
                gioHang.Remove(item);
                HttpContext.Session.Set(CART_KEY, gioHang);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult ThanhToan()
        {
            if (Carts.Count == 0)
            {
                ModelState.AddModelError("error", "giỏ hàng của bạn đang trống !");
                return Redirect("/Cart/Index");
            }
            // check khach hang co dang nhap khong
            CartCustomer cartCustomer = new CartCustomer();
            cartCustomer.Carts = Carts;
            var customerId = HttpContext.User.Claims.SingleOrDefault(p => p.Type == MySetting.CLAIM_CUSTOMERID)?.Value;
            if (customerId != null)
            {
                var kh = db.Khachhangs.SingleOrDefault(p=>p.Id.Equals(int.Parse(customerId)));
                
                cartCustomer.khachhang = kh;
                
            }
            // khach hang dat hang khong can dang nhap
            return View(cartCustomer);
        }
        [HttpPost]
        public IActionResult ThanhToan(OrderVM? model)
        {
            if (ModelState.IsValid)
            {
                var customerId = HttpContext.User.Claims.SingleOrDefault(p => p.Type == MySetting.CLAIM_CUSTOMERID)?.Value;
                
 

                var order = new Order
                {
                    Khachhang = int.Parse(customerId),
                    Fullname = model.Fullname,
                    Phone = model.Phone,
                    Email = model.Email,
                    Position = model.Position,
                    Total = ((int)Carts.Sum(p => p.ThanhTien)),
                    Status = "Đặt hàng thành công",
                    DateCreate = DateTime.Now
                };

                db.Database.BeginTransaction();
                try
                {
                    db.Database.CommitTransaction();
                    db.Add(order);
                    db.SaveChanges();

                    var cthds = new List<ChitietBill>();

                    foreach (var item in Carts)
                    {
                        cthds.Add(new ChitietBill
                        {
                            Bill = order.Id,
                            Product = item.Id,
                            Quantity = item.SoLuong,
                            Price = item.Price,
                            Total = (int)item.ThanhTien
                        });
                    }
                    db.AddRange(cthds);
                    db.SaveChanges();

                    HttpContext.Session.Set<List<CartItem>>(CART_KEY, new List<CartItem>());
                    return Redirect("/");
                }
                catch
                {
                    db.Database.RollbackTransaction();
                }
            }
            return View(Carts);
        }

    }
}
