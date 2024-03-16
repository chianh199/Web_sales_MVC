using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC2.Data2;
using MVC2.ViewModels;

namespace MVC2.ViewComponents
{
    public class MenuCataLog : ViewComponent
    {   //khai bao de su dung
        private readonly Food2Context db;
        //Read Database
        public MenuCataLog(Food2Context context) => db = context;

        public IViewComponentResult Invoke()
        {
            var data = db.Catalogs.Select(cate => new MenuCataLogVM
            {
                ID = cate.Id,
                Name = cate.Name,
                SoLuong = cate.Products.Count()
            });
            return View(data);
        }
    }
}
