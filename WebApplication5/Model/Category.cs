namespace WebApplication5.Model
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }

        public ICollection<Product> Products { get; set; }
        public ICollection<OrderCategory> OrderCategories { get; set; } = [];

    }
}
