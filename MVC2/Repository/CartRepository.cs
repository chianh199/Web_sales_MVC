using AutoMapper;
using MVC2.Data2;
using MVC2.Helpers;
using MVC2.Interfaces;
using MVC2.ViewModels;

namespace MVC2.Repository
{
    public class CartRepository : ICartRepository
    {
		const string CART_KEY = "MYCART";
		private readonly Food2Context _db;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _context;
		private readonly HttpContext httpContext;
		public List<CartItem> Carts => httpContext.Session.Get<List<CartItem>>(CART_KEY) ?? new List<CartItem>();

		public CartRepository(IHttpContextAccessor context, Food2Context context1, IMapper mapper)
		{
			_db = context1;
			_mapper = mapper;
			_context = context;
			httpContext = context.HttpContext;
		}
		public List<CartItem> AddToCart(int id, int quantity, string thu)
		{
            var gioHang = new List<CartItem>();
            if (thu != "")
			{
                foreach (var cart in Carts)
				{
					if(cart.user == thu)
					{
						gioHang.Add(cart);
					}
				}
			}
			else
			{
				gioHang = Carts;
			}			
			var item = gioHang.SingleOrDefault(p => p.Id == id);
			if (item != null)// co product nay
			{
				item.SoLuong += quantity;
				//return Carts;
			}
			else
			{
                var hangHoa = _db.Products.SingleOrDefault(p => p.Id == id);
				var item2 = _mapper.Map<CartItem>(hangHoa);
				item2.SoLuong = quantity;
				item2.user = thu;
                //var item1 = new CartItem
                //{
                //    Id = hangHoa.Id,
                //    Name = hangHoa.Name,
                //    Price = hangHoa.Price ?? 0,
                //    Image = hangHoa.Image ?? "",
                //    SoLuong = quantity
                //};
                gioHang.Add(item2);
            }
			httpContext.Session.Set(CART_KEY, gioHang);
			return Carts;
		}

		public void RemoveCart(int id, int ajax)
		{
            var gioHang = Carts;          	
			var item = gioHang.SingleOrDefault(p => p.Id == id);
			if (item != null)
			{
                if (ajax == 1)
                {
					item.SoLuong -= 1;
                }
				else
				{
                    gioHang.Remove(item);
                }            
				httpContext.Session.Set(CART_KEY, gioHang);
			}
		}
		public CartCustomer ThanhToan(int? idkh)
		{
			// check khach hang co dang nhap khong
			CartCustomer cartCustomer = new CartCustomer();
			cartCustomer.Carts = Carts;
			//var customerId = httpContext.User.Claims.SingleOrDefault(p => p.Type == MySetting.CLAIM_CUSTOMERID)?.Value;
			if (idkh != 0)
			{
				var kh = _db.Khachhangs.SingleOrDefault(p => p.Id.Equals(idkh));
				cartCustomer.khachhang = kh;
			}
			// khach hang dat hang khong can dang nhap
			return cartCustomer;
		}
		public void ThanhToan(OrderVM? model)
		{

			//var customerId = httpContext.User.Claims.SingleOrDefault(p => p.Type == MySetting.CLAIM_CUSTOMERID)?.Value;
			var order1 = _mapper.Map<Order>(model);
			order1.Status = "Đặt hàng thành công";
			order1.DateCreate = DateTime.Now;

   //         var order = new Order
			//{
			//	Khachhang = model.Khachhang,
			//	Fullname = model.Fullname,
			//	Phone = model.Phone,
			//	Email = model.Email,
			//	Position = model.Position,
			//	Total = ((int)Carts.Sum(p => p.ThanhTien)),
			//	Status = "Đặt hàng thành công",
			//	DateCreate = DateTime.Now
			//};			

			_db.Database.BeginTransaction();
			try
			{
				_db.Database.CommitTransaction();
				_db.Add(order1);
				_db.SaveChanges();

				var cthds = new List<ChitietBill>();

				foreach (var item in Carts)
				{
					//cthds.Add(_mapper.Map<ChitietBill>(item));
					cthds.Add(new ChitietBill
					{
						Bill = order1.Id,
						Product = item.Id,
						Quantity = item.SoLuong,
						Price = item.Price,
						Total = (int)item.ThanhTien
					});
				}
				_db.AddRange(cthds);
				_db.SaveChanges();

				httpContext.Session.Set<List<CartItem>>(CART_KEY, new List<CartItem>());
					//return Redirect("/");
			}
			catch
			{
				_db.Database.RollbackTransaction();
			}
		}

		public List<CartItem> GetCartItems()
		{			
			return Carts;
		}
	}
}


