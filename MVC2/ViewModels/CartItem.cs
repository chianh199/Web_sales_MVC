namespace MVC2.ViewModels
{
	public class CartItem
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Image { get; set; }
		public int Price { get; set; }
		public int SoLuong { get; set; }
		public double ThanhTien => SoLuong * Price;
		public string? user {  get; set; }
	}
}
