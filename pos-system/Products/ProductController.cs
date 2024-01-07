using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pos_system.Contexts;
using pos_system.Reservation;

namespace pos_system.Products
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly PosContext _context;

        public ProductController(PosContext _context)
        {
            this._context = _context;
        }

        [HttpPost]
        [Produces("application/json")]
        public async Task<ActionResult<ProductModel>> CreateProduct([FromBody] ProductPostRequestModel productPostRequest)
        {
            ProductModel product = new ProductModel
            {
                Id = Guid.NewGuid().ToString(),
                Name = productPostRequest.Name,
                Price = productPostRequest.Price,
                CategoryType = productPostRequest.CategoryType,
            };
            _context.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductModel>?> GetProduct(string id)
        {
            ProductModel? product = new ProductModel();
            product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                return product;
            }
            else
            {
                return NotFound();
            }
        }
        [HttpGet]
        public async Task<ProductModel[]> GetAllProducts()
        {
            ProductModel[] products = new ProductModel[0];
            if (_context != null)
            {
                products = await _context.Products.ToArrayAsync();
            }

            return products;
        }
        [HttpDelete("{id}")]
        public async Task<bool> DeleteProduct(string id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return false;
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return true;
        }
        [HttpPut("{productId}")]
        public async Task<ProductModel?> UpdateProduct(string productId, ProductPostRequestModel productUpdateModel)
        {
            ProductModel? updated = new ProductModel();
            if (_context != null)
            {
                updated = await _context.Products.SingleOrDefaultAsync(prod => prod.Id == productId);
                if (updated == null)
                {
                    return null;
                }
                if (productUpdateModel.Name != null)
                {
                    updated.Name = productUpdateModel.Name;
                }
                if (productUpdateModel.Price != null)
                {
                    updated.Price = productUpdateModel.Price;
                }
                if (productUpdateModel.CategoryType != null)
                {
                    updated.CategoryType = productUpdateModel.CategoryType;
                }
                await _context.SaveChangesAsync();
                return updated;
            }
            return null;

        }
        [HttpGet("bycategory/{categoryType}")]

        public async Task<ProductModel[]> GetProductsByCategory(string categoryType)
        {
            if (_context != null)
            {
                var productsByCategory = await _context.Products.Where(product => product.CategoryType == categoryType).ToArrayAsync();
                return productsByCategory;
            }
            return null;
        }

    }
}
