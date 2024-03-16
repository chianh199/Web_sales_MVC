using Microsoft.AspNetCore.Mvc;
using MVC2.ViewModels;
using MVC2.Data2;

namespace MVC2.Controllers
{
    public class Menu : Controller
    {
        private readonly Food2Context db;

        public Menu(Food2Context context) => db = context;
        public IActionResult Index(int? loai)
        {
            var products = db.Products.AsQueryable();
            if (loai.HasValue)
            {
                products = products.Where(p => p.Catalog == loai.Value);
            }
            var result = products.Select(p => new ProductVM
            {
                Id = p.Id,
                Name = p.Name ?? "",
                Describe = p.Describe ?? "",
                image = p.Image ?? "",
                Price = p.Price,
                catelog = p.CatalogNavigation.Name ?? ""
            });
            return View(result);
        }

        public IActionResult Search(string? query)
        {
            var products = db.Products.AsQueryable();
            if (query != null)
            {
                products = products.Where(p => p.Name.Contains(query));
            }
            var result = products.Select(p => new ProductVM
            {
                Id = p.Id,
                Name = p.Name,
                Describe = p.Describe ?? "",
                image = p.Image ?? "",
                Price = p.Price,
                catelog = p.CatalogNavigation.Name
            });
            return View(result);
        }
        public IActionResult Detail(int id)
        {
            var products = db.Products.AsQueryable().SingleOrDefault(p => p.Id == id);
            var cate = db.Catalogs.SingleOrDefault(p => p.Id == products.Catalog);
            if (products == null)
            {
                TempData["message"] = $"Not found items";
                return Redirect("/404");
            }
            var result = new ProductDetailVM
            {
                Id = products.Id,
                Name = products.Name ?? "",
                image = products.Image ?? "",
                Describe = products.Describe ?? "",
                Price = (int)products.Price,
                catelog = cate.Name ?? "",
                PointEvaluation = 5,
                SoLuongTon = 10
            };
            return View(result);
        }
    }
}
