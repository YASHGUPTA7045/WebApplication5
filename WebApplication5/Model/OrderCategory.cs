﻿namespace WebApplication5.Model
{
    public class OrderCategory
    {
        public int OrderId { get; set; }
        public Order orders { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
