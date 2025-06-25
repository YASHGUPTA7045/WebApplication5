using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication5.Data;
using WebApplication5.Model;
using WebApplication5.Model.DTO;

namespace WebApplication5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly AppDBContext _context;
        public CategoryController(AppDBContext context)
        {
            _context = context;

        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryReadDto>>> GetAllCategory()
        {
            var cate = await _context.categories.Include(x => x.Products)
               .Select(x => new CategoryReadDto
               {
                   CategoryName = x.CategoryName,
                   CategoryId = x.CategoryId,
                   Products = x.Products.Select(x => new ProductInCategoryDto
                   {
                       ProductId = x.ProductId,
                       ProductName = x.ProductName,
                       ProductPrice = x.ProductPrice
                   })
               })
                .ToListAsync();
            return Ok(cate);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryReadDto>> GetbyId(int id)
        {
            var cate = await _context.categories.Include(x => x.Products)
                .Where(c => c.CategoryId == id).
                Select(x => new CategoryReadDto
                {
                    CategoryId = x.CategoryId,
                    CategoryName = x.CategoryName,
                    Products = x.Products.Select(p => new ProductInCategoryDto
                    {
                        ProductId = p.ProductId,
                        ProductName = p.ProductName,
                        ProductPrice = p.ProductPrice
                    }).ToList()
                }).FirstOrDefaultAsync();

            if (cate == null)
            {
                return NotFound("not available");
            }
            return Ok(cate);

        }
        [HttpPost]
        public async Task<ActionResult<CategoryCreateDto>> AddCategory(CategoryCreateDto xyz)
        {
            var cate = new Category
            {
                CategoryName = xyz.CategoryName
            };
            await _context.categories.AddAsync(cate);
            await _context.SaveChangesAsync();
            var result = new CategoryCreateDto
            {

                CategoryName = cate.CategoryName
            };
            return Ok(result);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCategory(int id, CategoryUpdateDto xyz)
        {
            var category = await _context.categories.FirstOrDefaultAsync(c => c.CategoryId == id);

            if (category == null)
            {
                return NotFound("Category not available");
            }


            category.CategoryName = xyz.CategoryName;

            await _context.SaveChangesAsync();
            var result = new CategoryUpdateDto
            {
                CategoryName = category.CategoryName
            };


            return Ok(result);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> CategoryDeleted(int id)
        {
            var del = await _context.categories.FindAsync(id);
            if (del == null)
            {
                return NotFound("data not available");
            }
            _context.categories.Remove(del);
            await _context.SaveChangesAsync();
            return Ok(del);
        }


    }
}
