using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pos_system.Contexts;
using pos_system.Products;

namespace pos_system.Discounts
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountController : Controller
    {
        private readonly PosContext _context;
        public DiscountController(PosContext _context)
        {
            this._context = _context;
        }

        [HttpGet]
        public async Task<ActionResult<DiscountModel[]>> GetAllDiscounts() {
            DiscountModel[] allDiscounts = await _context.Discounts.ToArrayAsync();

            return allDiscounts;
        }

        [HttpPost]
        [Produces("application/json")]
        public async Task<ActionResult<DiscountModel>> CreateDiscount([FromBody] DiscountPostRequestModel discountModel)
        {
            DiscountModel discount = new DiscountModel
            {
                Id = Guid.NewGuid().ToString(),
                ValidUntilDateTime = discountModel.ValidUntilDateTime,
                Percentage = discountModel.Percentage,
            };

            _context.Add(discount);
            await _context.SaveChangesAsync();
            return discount;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DiscountModel>> GetDiscountById(String id)
        {
            DiscountModel? discount = new DiscountModel();
            discount = await _context.Discounts.FindAsync(id);
            if (discount != null) {
                return discount;
            }
            return NotFound();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<DiscountModel?>> UpdateDiscount(String id, DiscountPostRequestModel discountModel)
        {
            DiscountModel? discount = new DiscountModel();
            discount = await _context.Discounts.FindAsync(id);

            if (discount == null) {
                return NotFound();
            }
            if (discountModel.ValidUntilDateTime != null) {
                discount.ValidUntilDateTime = discountModel.ValidUntilDateTime;
            }
            if (discountModel.Percentage != null) {
                discount.Percentage = discountModel.Percentage;
            }
            await _context.SaveChangesAsync();
            return discount;

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteDiscount(String id)
        {
            DiscountModel? discount = new DiscountModel();
            discount = await _context.Discounts.FindAsync(id);
            
            if (discount == null) {
                return NotFound();
            }

            _context.Discounts.Remove(discount);
            await _context.SaveChangesAsync();

            return NotFound();
        }

        [HttpGet("{id}/products")]
        public async Task<ActionResult<List<DiscountProductModel>?>> GetDiscountedProducts(String id)
        {
            List<DiscountProductModel>? products = new List<DiscountProductModel>();
            products = await _context.DiscountProducts.Where(p => p.DiscountId == id).ToListAsync();

            return products;
        }

        [HttpPost("{id}/products")]
        public async Task<ActionResult<DiscountProductModel>> AddProductToDiscount(String id, [FromBody] DiscountProductPostRequestModel discountRequestModel)
        {
            var existingProduct = await _context.Products.FindAsync(discountRequestModel.ProductId);
            var existingDiscount = await _context.Discounts.FindAsync(id);

            if (existingProduct == null || existingDiscount == null) {
                return NotFound();
            }

            DiscountProductModel product = new DiscountProductModel()
            {
                Id = Guid.NewGuid().ToString(),
                DiscountId = id,
                ProductId = discountRequestModel.ProductId,
                Name = existingProduct.Name,
            };

            _context.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }

        [HttpDelete("{discountId}/products/{productId}")]
        public async Task<ActionResult> DeleteDiscountOnProduct(String discountId, String productId)
        {
            DiscountProductModel? discountProduct = new DiscountProductModel();
            discountProduct = await _context.DiscountProducts.FirstOrDefaultAsync(dp => dp.ProductId == productId && dp.DiscountId == discountId);
        
            if (discountProduct == null)
            {
                return NotFound();
            }

            _context.Remove(discountProduct);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpGet("{id}/services")]
        public async Task<ActionResult<List<DiscountServiceModel>?>> GetDiscountedServices(String id)
        {
            List<DiscountServiceModel>? services = new List<DiscountServiceModel>();
            services = await _context.DiscountServices.Where(s => s.DiscountId == id).ToListAsync();

            return services;
        }

        [HttpPost("{id}/services")]
        public async Task<ActionResult<DiscountServiceModel>> AddServiceToDiscount(String id, [FromBody] DiscountServicePostRequestModel discountRequestModel)
        {
            var existingService = await _context.Services.FindAsync(discountRequestModel.ServiceId);
            var existingDiscount = await _context.Discounts.FindAsync(id);

            if (existingService == null || existingDiscount == null)
            {
                return NotFound();
            }

            DiscountServiceModel service = new DiscountServiceModel()
            {
                Id = Guid.NewGuid().ToString(),
                DiscountId = id,
                ServiceId = discountRequestModel.ServiceId,
                Name = existingService.Name,
            };

            _context.Add(service);
            await _context.SaveChangesAsync();
            return service;
        }

        [HttpDelete("{discountId}/services/{serviceId}")]
        public async Task<ActionResult> DeleteDiscountOnService(String discountId, String serviceId)
        {
            DiscountServiceModel? discountService = new DiscountServiceModel();
            discountService = await _context.DiscountServices.FirstOrDefaultAsync(ds => ds.ServiceId == serviceId && ds.DiscountId == discountId);

            if (discountService == null)
            {
                return NotFound();
            }

            _context.Remove(discountService);
            await _context.SaveChangesAsync();

            return Ok();
        }


    }
}
