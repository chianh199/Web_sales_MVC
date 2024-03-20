using MVC2.ViewModels;

namespace MVC2.Interfaces
{
    public interface ICartRepository
    {
        public bool AddToCart(int id, int quantity);
        public bool RemoveCart(int id);
        public CartCustomer GetCartCustomer(int id);
        public bool ThanhToan(OrderVM? model);
    }
}
