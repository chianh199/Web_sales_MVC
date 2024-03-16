using Microsoft.AspNetCore.Mvc;
using MVC2.ViewModels;
using MVC2.Helpers;

namespace MVC2.ViewComponents
{
    public class IconCart : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            const string CART_KEY = "MYCART";
            var Carts = HttpContext.Session.Get<List<CartItem>>(CART_KEY) ?? new List<CartItem>();
            return View("IconCart", new IconCartVM
            {
                TotalCartItem = Carts.Sum(p => p.SoLuong)
            });
        }

    }
}
