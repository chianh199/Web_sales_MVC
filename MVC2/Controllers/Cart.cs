using Microsoft.AspNetCore.Mvc;
using MVC2.ViewModels;
using MVC2.Interfaces;
using Microsoft.AspNetCore.Http;
using MVC2.Helpers;
using Humanizer;

namespace MVC2.Controllers
{
	public class Cart : Controller
	{
		private readonly ICartRepository _cart;

		public Cart(ICartRepository cartRepository)
        {
            _cart = cartRepository;
        }
        

        public IActionResult Index()
        {
            var check = _cart.GetCartItems();
            return View(check);
        }
        [HttpPost]
        public IActionResult GetItemCart()
        {          
            var check = _cart.GetCartItems();
            ViewData["thu"] = check.Sum(i => i.ThanhTien);
            return PartialView("GetItemCart", check);
        }
        public IActionResult AddToCart(int id, int quantity = 1)
        {
            var customer = HttpContext.User.Identity.Name ?? "";
            var add = _cart.AddToCart(id, quantity, customer);
            return RedirectToAction("Index");
        }
        public IActionResult RemoveCart(int id, int ajax=0)
        {
            _cart.RemoveCart(id,ajax);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult ThanhToan(int kh = 1)
        {
           
            var check = _cart.GetCartItems();
            if(check.Count() == 0) {
                return Redirect("/Cart");
            }
            var customerId = HttpContext.User.Claims.SingleOrDefault(p => p.Type == MySetting.CLAIM_CUSTOMERID)?.Value??"0";
            if (customerId == "0" && kh ==1)
            {
                ViewBag.mess = "1";
                return Redirect("/KhachHang/Login?tt=1");
            }
            var thu = int.Parse(customerId);
            var checklogin = _cart.ThanhToan(int.Parse(customerId));
            return View(checklogin);
        }
        [HttpPost]
        public IActionResult ThanhToan(OrderVM? model)
        {
            if (ModelState.IsValid)
            {
               _cart.ThanhToan(model);
            }
            return Redirect("/Cart");
        }

    }
}
