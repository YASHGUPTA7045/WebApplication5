using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication5.Data;
using WebApplication5.Model;
using WebApplication5.Model.DTO;

namespace WebApplication5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly AppDBContext _context;
        public ProductController(AppDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductReadDto>>> GetAllProduct(int PageNumber = 1, int PageSize = 10)
        {
            if (PageNumber <= 0 || PageSize <= 0) return BadRequest("Enter Valid imput");
            var result = await _context.Products.Include(y => y.Category).Skip((PageNumber - 1) * PageSize)
        .Take(PageSize)
                .Select(x => new ProductReadDto
                {
                    ProductId = x.ProductId,
                    ProductName = x.ProductName,
                    ProductPrice = x.ProductPrice,
                    CategoryId = x.CategoryId,
                    CategoryName = x.Category.CategoryName

                })

                .ToListAsync();
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductReadDto>> GetById(int id)
        {
            var result = await _context.Products.Include(x => x.Category)
                .Where(x => x.ProductId == id).Select(x => new ProductReadDto
                {
                    ProductId = x.ProductId,
                    ProductName = x.ProductName,
                    ProductPrice = x.ProductPrice,
                    CategoryId = x.CategoryId,
                    CategoryName = x.Category.CategoryName
                }).FirstOrDefaultAsync();
            if (result == null)
            {
                return NotFound("data not available");
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<ProductCreateDto>> CreateProduct(ProductCreateDto xyz)
        {
            var create = new Product
            {
                ProductName = xyz.ProductName,
                ProductPrice = xyz.ProductPrice,
                CategoryId = xyz.CategoryId,
            };
            await _context.Products.AddAsync(create);
            await _context.SaveChangesAsync();
            var dto = new ProductCreateDto
            {
                ProductName = create.ProductName,
                ProductPrice = create.ProductPrice,
                CategoryId = create.CategoryId
            };
            return Ok(dto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var delete = await _context.Products.FindAsync(id);
            if (delete == null)
            {
                return NotFound();
            }
            _context.Products.Remove(delete);
            await _context.SaveChangesAsync();
            return Ok(delete);
        }



        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateProduct(int id, ProductUpdateDto xyz)
        {
            var data = await _context.Products
                .Where(x => x.ProductId == id).FirstOrDefaultAsync();
            if (data == null)
            {
                return NotFound();
            }
            data.ProductName = xyz.ProductName;
            data.ProductPrice = xyz.ProductPrice;
            data.CategoryId = xyz.CategoryId;
            await _context.SaveChangesAsync();

            var dto = new ProductReadDto
            {
                ProductId = data.ProductId,
                ProductName = data.ProductName,
                ProductPrice = data.ProductPrice,
                CategoryId = data.CategoryId,

            };

            return Ok(dto);
        }

    }
}
