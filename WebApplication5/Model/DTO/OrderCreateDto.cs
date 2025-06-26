namespace WebApplication5.Model.DTO
{
    public class OrderCreateDto
    {

        public string OrderName { get; set; }
        public List<int> ProductIds { get; set; }
        public List<int> CategoryIds { get; set; }
    }
}
