using Microsoft.AspNetCore.Mvc;
using MVC2.ViewModels;
using MVC2.Data2;
using X.PagedList;

namespace MVC2.Controllers
{
    public class Menu : Controller
    {
        private readonly Food2Context db;

        public Menu(Food2Context context) => db = context;
        public IActionResult Index(int? loai, int? page)
        {
            var products = db.Products.AsQueryable();
            if (loai.HasValue)
            {
                products = products.Where(p => p.Catalog == loai.Value);
            }
            var result = products.OrderBy(p=>p.Catalog).Select(p => new ProductVM
            {
                Id = p.Id,
                Name = p.Name ?? "",
                Describe = p.Describe ?? "",
                image = p.Image ?? "",
                Price = p.Price,
                catelog = p.CatalogNavigation.Name ?? ""
            });
            int pagesize = 6;
            int pagenumber = page == null || page < 0 ? 1 : page.Value;
            PagedList<ProductVM> lst = new PagedList<ProductVM>(result, pagenumber, pagesize);
            return View(lst);
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
