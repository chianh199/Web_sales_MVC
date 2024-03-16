using MVC2.Data2;

namespace MVC2.ViewModels
{
    public class CartCustomer
    {
        public List<CartItem> Carts { get; set; }
        public Khachhang? khachhang { get; set; }
    }
}
