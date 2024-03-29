using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MVC2.Data2;
using MVC2.ViewModels;
using System.Text;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using MVC2.Helpers;
using MVC2.Interfaces;


namespace MVC2.Controllers
{
    public class KhachHang : Controller
    {
        private readonly Food2Context db;
        private readonly IMapper _mapper;
        private readonly ICartRepository _cart;
        const string CART_KEY = "MYCART";
        public List<CartItem> Carts => HttpContext.Session.Get<List<CartItem>>(CART_KEY) ?? new List<CartItem>();
        public KhachHang(Food2Context context, IMapper mapper, ICartRepository cart)
        {
            db = context;
            _mapper = mapper;
            _cart = cart;
        }
        [HttpGet]
        public IActionResult DangKy()
        {
            return View();
        }
        [HttpPost]
        public IActionResult DangKy(RegisterVM register)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    var kh = _mapper.Map<Khachhang>(register);
                    //kh.Id = db.Khachhangs.Max(k => k.Id) + 1;

                    byte[] buffer = Encoding.UTF8.GetBytes(kh.Pwd);
                    MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
                    buffer = md5.ComputeHash(buffer);
                    kh.Pwd = null;
                    for (int i = 0; i < 9; i++)
                    {
                        kh.Pwd += buffer[i].ToString("x1");
                    }
                    db.Khachhangs.Add(kh);
                    db.SaveChanges();
                    return RedirectToAction("Index", "Menu");
                }
                catch (Exception ex)
                {
                    var mess = $"{ex.Message} shh";
                }
            }
            return View();
        }
        [HttpGet]
        public IActionResult Login(string? ReturnUrl, int? tt)
        {
            ViewBag.ReturnUrl = ReturnUrl;
            ViewBag.tt = tt;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string? ReturnUrl, LoginVM login)
        {
            ViewBag.ReturnUrl = ReturnUrl;
            if (ModelState.IsValid)
            {
                byte[] buffer = Encoding.UTF8.GetBytes(login.password);
                MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
                buffer = md5.ComputeHash(buffer);
                login.password = null;
                for (int i = 0; i < 9; i++)
                {
                    login.password += buffer[i].ToString("x1");
                }
                var user = await db.Khachhangs.SingleOrDefaultAsync(kh => kh.Email == login.username && kh.Pwd == login.password);
                if (user == null)
                {
                    ModelState.AddModelError("user", "Not found your account !");
                    return View();
                }

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Email),
                    new Claim(MySetting.CLAIM_CUSTOMERID, user.Id.ToString())
                };
                var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                await HttpContext.SignInAsync(claimsPrincipal);
                if(Carts.Count() != 0)
                {
                    foreach(var cart in Carts)
                    {
                        if (cart.user == "")
                        {
                            _cart.AddToCart(cart.Id, cart.SoLuong, user.Email ?? "");
                        }                      
                    }
                }
                if (Url.IsLocalUrl(ReturnUrl))
                {
                    return Redirect(ReturnUrl);
                }
                return Redirect("/");

            }
            return View();
        }

        [Authorize]
        public IActionResult Profile()
        {
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/");
        }
    }
}
