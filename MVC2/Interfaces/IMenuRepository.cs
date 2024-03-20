using MVC2.ViewModels;

namespace MVC2.Interfaces
{
    public interface IMenuRepository
    {
        public ProductVMs GetAllProducts(string? term, string? orderBy, int currentPage);
        public ProductDetailVM GetProduct(int id);

    }
}
