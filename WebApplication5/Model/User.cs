namespace WebApplication5.Model
{
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserAddress { get; set; }
        public ICollection<Order> orders { get; set; }

        public string Email { get; set; }
        public string Password { get; set; }




    }
}
