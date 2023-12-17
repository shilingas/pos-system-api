using Microsoft.AspNetCore.Mvc;
using pos_system.Contexts;
using pos_system.Models;
using Microsoft.EntityFrameworkCore;

namespace pos_system.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly PosContext? _context;
        public ProductController(PosContext? context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ProductModel[]> GetAllProducts()
        {
            var products = await _context.Products.ToArrayAsync();
            return products;
        }
        [HttpPost]
        [Produces("application/json")]
        public async Task<IActionResult> CreateOrder([FromBody] ProductModel product)
        {
            _context.Add(product);
            await _context.SaveChangesAsync();
            return Ok(product);
        }
    }
}
