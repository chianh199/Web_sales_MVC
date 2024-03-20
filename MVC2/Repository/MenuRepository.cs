using AutoMapper;
using MVC2.Data2;
using MVC2.Interfaces;
using MVC2.ViewModels;

namespace MVC2.Repository
{
    public class MenuRepository : IMenuRepository
    {
        private readonly Food2Context _db;
        private readonly IMapper _mapper;

        public MenuRepository(Food2Context context, IMapper mapper) {
            _db = context;
            _mapper = mapper;
        }
        public ProductVMs GetAllProducts(string? term, string? orderBy, int currentPage)
        {
            term = string.IsNullOrEmpty(term) ? "" : term.ToLower();
            var ds = new ProductVMs();
            ds.IncreaseSortOrder = string.IsNullOrEmpty(orderBy) ? "" : "increase";
            ds.DecreaseSortOrder = orderBy == "decrease" ? "decrease" : "";

            var dsp = (from p in _db.Products
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
            return ds;
        }

        public ProductDetailVM GetProduct(int id)
        {
            var products = _db.Products.AsQueryable().SingleOrDefault(p => p.Id == id);
            var cate = _db.Catalogs.SingleOrDefault(p => p.Id == products.Catalog);          
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
            return result;
        }
    }
}
