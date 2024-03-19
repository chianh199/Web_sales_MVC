namespace MVC2.ViewModels
{
	public class ProductVM
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Describe { get; set; }
		public string image { get; set; }
		public int? Price { get; set; }
		public string catelog { get; set; }
	}
	public class ProductVMs
	{
		public IQueryable<ProductVM> ds { get; set;}
		public string IncreaseSortOrder { get; set; }
		public string DecreaseSortOrder { get; set; }
		public int PageSize { get; set; }
		public int CurrentPage { get; set; }
		public int TotalPages { get; set; }
		public string Term { get; set; }
		public string OrderBy { get; set; }
	}
}
