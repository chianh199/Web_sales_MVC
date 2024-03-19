using Microsoft.AspNetCore.Mvc;
using MVC2.ViewModels;
using MVC2.Data2;
using X.PagedList;


namespace MVC2.Controllers
{
    public class Menu : Controller
    {
        private readonly Food2Context db;       
        public Menu(Food2Context context)
        {
			db = context;			
		}      
        public IActionResult Index(string term = "", string orderBy = "", int currentPage = 1)
        {
			term = string.IsNullOrEmpty(term) ? "" : term.ToLower();
            var ds = new ProductVMs();
			ds.IncreaseSortOrder = string.IsNullOrEmpty(orderBy) ? "" : "increase";
			ds.DecreaseSortOrder = orderBy == "decrease" ? "decrease" : "";

			var dsp = (from p in db.Products
							 where term == "" || p.Name.ToLower().StartsWith(term)
							 select new ProductVM
							 {
								 Id = p.Id,
								 Name = p.Name ?? "",
								 Describe = p.Describe ?? "",
								 image = p.Image ?? "",
								 Price = p.Price,
								 catelog = p.CatalogNavigation.Name ?? ""
							 }
							);

			switch (orderBy)
			{
				case "decrease":
					dsp = dsp.OrderByDescending(a => a.Price);
					break;
				default:
					dsp = dsp.OrderBy(a => a.Price);
					break;
			}
			int totalRecords = dsp.Count();
			int pageSize = 6;
			int totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);
			dsp = dsp.Skip((currentPage - 1) * pageSize).Take(pageSize);

			ds.ds = dsp;
			ds.CurrentPage = currentPage;
			ds.TotalPages = totalPages;
			ds.Term = term;
			ds.PageSize = pageSize;
			ds.OrderBy = orderBy;
			//var result = products.OrderBy(p=>p.Catalog).Select(p => new ProductVM
   //         {
   //             Id = p.Id,
   //             Name = p.Name ?? "",
   //             Describe = p.Describe ?? "",
   //             image = p.Image ?? "",
   //             Price = p.Price,
   //             catelog = p.CatalogNavigation.Name ?? ""
   //         });
   //         //sort
            
   //         int pagesize = 6;
   //         int pagenumber = page == null || page < 0 ? 1 : page.Value;
   //         PagedList<ProductVM> lst = new PagedList<ProductVM>(result, pagenumber, pagesize);
            return View(ds);
        }
        
        public IActionResult Search(string? query, int? page)
        {
            var products = db.Products.AsQueryable();
            if (query != null)
            {
                products = products.Where(p => p.Name.Contains(query));
            }
            var result = products.OrderBy(p=>p.Catalog).Select(p => new ProductVM
            {
                Id = p.Id,
                Name = p.Name,
                Describe = p.Describe ?? "",
                image = p.Image ?? "",
                Price = p.Price,
                catelog = p.CatalogNavigation.Name
            });
            int pagesize = 6;
            int pagenumber = page == null || page < 0 ? 1 : page.Value;
            PagedList<ProductVM> lst = new PagedList<ProductVM>(result, pagenumber, pagesize);
            return View(lst);
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
