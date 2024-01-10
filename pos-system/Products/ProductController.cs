using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pos_system.Contexts;
using pos_system.Customers;
using pos_system.Reservation;

namespace pos_system.Products
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly PosContext _context;
        private readonly IProductService _productService;

        public ProductController(PosContext _context, IProductService productService)
        {
            this._context = _context;
            _productService = productService;
        }

        [HttpPost]
        [Produces("application/json")]
        public async Task<ActionResult<ProductModel>> CreateProduct([FromBody] ProductPostRequestModel productPostRequest)
        {
            ProductModel? product = await _productService.CreateProduct(productPostRequest);
            if (product == null)
            {
                return BadRequest();
            }
            return Ok(product);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductModel>?> GetProduct(string id)
        {
            ProductModel? product = await _productService.GetProduct(id);
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
            return await _productService.GetAllProducts();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(string id)
        {
            bool isDeleted = await _productService.DeleteProduct(id);
            if (isDeleted)
            {
                return NoContent();
            }
            return NotFound();
        }

        [HttpPut("{productId}")]
        public async Task<ActionResult> UpdateProduct(string productId, ProductPostRequestModel productUpdateModel)
        {
            ProductModel? product = await _productService.UpdateProduct(productId, productUpdateModel);
            if (product != null)
            {
                return Ok(product);
            }
            else
            {
                return NotFound();
            }

        }
        [HttpGet("bycategory/{categoryType}")]

        public async Task<ProductModel[]> GetProductsByCategory(string categoryType)
        {
            return await _productService.GetProductsByCategory(categoryType);
        }

    }
}
