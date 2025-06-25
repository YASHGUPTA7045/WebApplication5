namespace WebApplication5.Model
{
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int ProductPrice { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
