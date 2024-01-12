using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pos_system.Contexts;
using pos_system.ProductService.Products;

namespace pos_system.ProductService.Discounts
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountController : Controller
    {
        private readonly IDiscountService discountService;
        public DiscountController(IDiscountService _discountService)
        {
            discountService = _discountService;
        }

        [HttpGet]
        public async Task<ActionResult<DiscountModel[]>> GetAllDiscounts()
        {
            DiscountModel[] allDiscounts = await discountService.GetAllDiscounts();

            return allDiscounts;
        }

        [HttpPost]
        [Produces("application/json")]
        public async Task<ActionResult<DiscountModel>> CreateDiscount([FromBody] DiscountPostRequestModel discountModel)
        {
            DiscountModel discount = await discountService.CreateDiscount(discountModel);
            return Ok(discount);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DiscountModel>> GetDiscountById(string id)
        {
            DiscountModel? discount = await discountService.GetDiscountById(id);
            if (discount != null)
            {
                return Ok(discount);
            }
            return NotFound();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<DiscountModel?>> UpdateDiscount(string id, DiscountPostRequestModel discountModel)
        {
            DiscountModel? discount = await discountService.UpdateDiscount(id, discountModel);

            if (discount == null)
            {
                return NotFound();
            }

            return Ok(discount);

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteDiscount(string id)
        {
            bool discount = await discountService.DeleteDiscount(id);

            if (discount == false)
            {
                return NotFound();
            }

            return Ok();
        }

        [HttpGet("{id}/products")]
        public async Task<ActionResult<List<DiscountProductModel>?>> GetDiscountedProducts(string id)
        {
            List<DiscountProductModel>? products = await discountService.GetDiscountedProducts(id);

            return Ok(products);
        }

        [HttpPost("{id}/products")]
        public async Task<ActionResult<DiscountProductModel?>> AddProductToDiscount(string id, [FromBody] DiscountProductPostRequestModel discountRequestModel)
        {
            DiscountProductModel? product = await discountService.AddProductToDiscount(id, discountRequestModel);

            if (product == null)
            {
                return BadRequest();
            }

            return product;
        }

        [HttpDelete("{discountId}/products/{productId}")]
        public async Task<ActionResult> DeleteDiscountOnProduct(string discountId, string productId)
        {
            bool discountProduct = await discountService.DeleteDiscountOnProduct(discountId, productId);

            if (discountProduct == false)
            {
                return NotFound();
            }

            return Ok();
        }

        [HttpGet("{id}/services")]
        public async Task<ActionResult<List<DiscountServiceModel>?>> GetDiscountedServices(string id)
        {
            List<DiscountServiceModel>? services = await discountService.GetDiscountedServices(id);

            return services;
        }

        [HttpPost("{id}/services")]
        public async Task<ActionResult<DiscountServiceModel>> AddServiceToDiscount(string id, [FromBody] DiscountServicePostRequestModel discountRequestModel)
        {
            DiscountServiceModel? service = await discountService.AddServiceToDiscount(id, discountRequestModel);

            if (service == null)
            {
                return BadRequest();
            }

            return service;
        }

        [HttpDelete("{discountId}/services/{serviceId}")]
        public async Task<ActionResult> DeleteDiscountOnService(string discountId, string serviceId)
        {
            bool discountOnService = await discountService.DeleteDiscountOnService(discountId, serviceId);

            if (discountOnService == false)
            {
                return NotFound();
            }

            return Ok();
        }


    }
}