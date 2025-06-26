namespace WebApplication5.Model.DTO
{
    public class OrderReadDto
    {
        public int OrderId { get; set; }
        public string OrderName { get; set; }
        public int UserId { get; set; }

        public string UserName { get; set; }
        public string UserAddress { get; set; }

        public List<CategoryReadDto> Categories { get; set; }


    }
}
