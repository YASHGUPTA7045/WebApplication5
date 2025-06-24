namespace WebApplication5.Model
{
    public class Order
    {
        public int OrderId { get; set; }
        public string OrderName { get; set; }
        public ICollection<Product> products { get; set; }
    }
}
