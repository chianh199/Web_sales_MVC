using Microsoft.AspNetCore.Mvc;
using MVC2.Interfaces;


namespace MVC2.Controllers
{
    public class Menu : Controller
    {      
        private readonly IMenuRepository _menu;

        public Menu(IMenuRepository menuRepository)
        {
            _menu = menuRepository;			
		}      
        public IActionResult Index(string term = "", string orderBy = "", int currentPage = 1)
        { 
            return View(_menu.GetAllProducts(term, orderBy, currentPage));
        }
           
        public IActionResult Detail(int id)
        {
            var productdetail = _menu.GetProduct(id);
            if(productdetail == null)
            {
                ModelState.AddModelError("404", "Not found this product");
                return View();
            }
            return View(productdetail);
        }
    }
}
