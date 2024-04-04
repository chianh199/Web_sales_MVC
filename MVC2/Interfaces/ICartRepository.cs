using MVC2.ViewModels;

namespace MVC2.Interfaces
{
    public interface ICartRepository
    {
		public List<CartItem> AddToCart(int id, int quantity, string thu);
		public List<CartItem> GetCartItems();

		public void RemoveCart(int id, int ajax);

		public CartCustomer ThanhToan(int? idkh);

		public void ThanhToan(OrderVM? model);

	}
}
