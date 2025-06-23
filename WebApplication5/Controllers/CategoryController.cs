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
            var cate = await _context.categories
                .Include(p => p.products).Select(x => new CategoryReadDto
                {
                    CategoryName = x.CategoryName
                })
                .ToListAsync();
            return Ok(cate);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetbyId(int id)
        {
            var cate = await _context.categories.FindAsync(id);
            if (cate == null)
            {
                return NotFound("not available");
            }
            return Ok(cate);

        }
        [HttpPost]
        public async Task<ActionResult<Category>> AddCategory(Category xyz)
        {
            var cate = new Category
            {
                CategoryName = xyz.CategoryName,
                CategoryId = xyz.CategoryId,


            };
            await _context.categories.AddAsync(cate);
            await _context.SaveChangesAsync();
            return Ok(cate);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCategory(int id, Category xyz)
        {
            var update = await _context.categories.FindAsync(id);
            if (update == null)
            {
                return NotFound("data not available");
            }
            update.CategoryName = xyz.CategoryName;
            await _context.SaveChangesAsync();
            return Ok(update);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> CategoryDeleted(int id)
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
