﻿namespace WebApplication5.Model
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }

        public ICollection<Product> products { get; set; }
    }
}
