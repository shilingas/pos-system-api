using Microsoft.AspNetCore.Mvc;
using pos_system.Contexts;
using Microsoft.EntityFrameworkCore;
using pos_system.ProductService.Services;

namespace pos_system.Customers.Coupons
{
    [Route("api/[controller]")]
    [ApiController]
    public class CouponController : Controller
    {
        private readonly ICouponService _couponService;
        public CouponController(PosContext _context, ICouponService couponService)
        {
            _couponService = couponService;
        }
        [HttpPost]
        [Produces("application/json")]
        public async Task<ActionResult<CouponModel>> CreateCoupon([FromBody] CouponPostRequestModel couponModel)
        {

            CouponModel? coupon = await _couponService.CreateCoupon(couponModel);
            if (coupon == null)
            {
                return BadRequest();
            }
            return Ok(coupon);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<CouponModel>?> GetCoupon(string id)
        {
            CouponModel? coupon = await _couponService.GetCoupon(id);
            if (coupon != null)
            {
                return coupon;
            }
            else
            {
                return NotFound();
            }
        }
        [HttpGet]
        public async Task<CouponModel[]> GetAllCoupons()
        {
            return await _couponService.GetAllCoupons();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCoupon(string id)
        {
            bool isDeleted = await _couponService.DeleteCoupon(id);
            if (isDeleted)
            {
                return NoContent();
            }
            return NotFound();
        }
        [HttpPut("{couponId}")]
        public async Task<ActionResult<ServiceModel>> UpdateCoupon(string couponId, CouponPostRequestModel couponModel)
        {
            CouponModel? coupon = await _couponService.UpdateCoupon(couponId, couponModel);
            if (coupon != null)
            {
                return Ok(coupon);
            }
            else
            {
                return NotFound();
            }

        }
    }
}
