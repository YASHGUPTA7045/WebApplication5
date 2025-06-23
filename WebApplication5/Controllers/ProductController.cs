using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication5.Data;
using WebApplication5.Model;

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
        public async Task<ActionResult<IEnumerable<Product>>> GetAllProduct()
        {
            var result = await _context.Products
                .Include(p => p.Category)
                .ToListAsync();
            return Ok(result);
        }
        [HttpGet("{Id}")]
        public async Task<ActionResult<Product>> GetById(int Id)
        {
            var result = await _context.Products.FindAsync(Id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        [HttpPost]
        public async Task<ActionResult<Product>> CreteProduct(Product xyz)
        {

            var obj = new Product
            {
                //ProductId = xyz.ProductId,
                ProductName = xyz.ProductName,
                ProductPrice = xyz.ProductPrice
            };
            _context.Products.Add(obj);
            await _context.SaveChangesAsync();
            return Ok(obj);

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
        public async Task<IActionResult> UpdateProduct(int id, Product xyz)
        {
            var updated = await _context.Products.FindAsync(id);
            if (updated == null)
            {
                return NotFound("data not found");
            }
            updated.ProductPrice = xyz.ProductPrice;
            updated.ProductName = xyz.ProductName;
            await _context.SaveChangesAsync();
            return Ok(updated);
        }

    }
}
