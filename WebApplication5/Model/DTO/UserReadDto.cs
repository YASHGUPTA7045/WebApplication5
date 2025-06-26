namespace WebApplication5.Model.DTO
{
    public class UserReadDto
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserAddress { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }


        public IEnumerable<OrderIUserDto> Orders { get; set; }

    }
}
