namespace WebApplication5.Model
{
    public class Order
    {
        public int OrderId { get; set; }
        public string OrderName { get; set; }
        public int UserId { get; set; }
        public User user { get; set; }
        public ICollection<OrderCategory> OrderCategories { get; set; } = new List<OrderCategory>();

    }
}
