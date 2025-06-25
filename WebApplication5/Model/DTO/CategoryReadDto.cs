namespace WebApplication5.Model.DTO
{
    public class CategoryReadDto
    {
        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }

        public IEnumerable<ProductInCategoryDto> Products { get; set; }

    }
}
