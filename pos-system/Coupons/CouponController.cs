﻿using Microsoft.AspNetCore.Mvc;
using pos_system.Contexts;
using Microsoft.EntityFrameworkCore;
using pos_system.Customers;

namespace pos_system.Coupons
{
    [Route("api/[controller]")]
    [ApiController]
    public class CouponController : Controller
    {
        private readonly PosContext _context;
        public CouponController(PosContext _context)
        {
            this._context = _context;
        }
        [HttpPost]
        [Produces("application/json")]
        public async Task<ActionResult<CouponModel>> CreateCoupon([FromBody] CouponPostRequestModel couponModel)
        {
            CouponModel customer = new CouponModel
            {
                Id = Guid.NewGuid().ToString(),
                Discount = couponModel.Discount,
                ValidUntilDateTime = couponModel.ValidUntilDateTime,
                Status = couponModel.Status,
            };
            _context.Add(customer);
            await _context.SaveChangesAsync();
            return customer;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<CouponModel>?> GetCoupon(string id)
        {
            CouponModel? coupon = new CouponModel();
            coupon = await _context.Coupons.FindAsync(id);
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
            CouponModel[] coupons = new CouponModel[0];
            if (_context != null)
            {
                coupons = await _context.Coupons.ToArrayAsync();
            }

            return coupons;
        }
        [HttpDelete("{id}")]
        public async Task<bool> DeleteCoupon(string id)
        {
            var coupon = await _context.Coupons.FindAsync(id);
            if (coupon == null)
            {
                return false;
            }

            _context.Coupons.Remove(coupon);
            await _context.SaveChangesAsync();

            return true;
        }
        [HttpPut("{couponId}")]
        public async Task<CouponModel?> UpdateCoupon(string couponId, CouponPostRequestModel couponModel)
        {
            CouponModel? updated = new CouponModel();
            if (_context != null)
            {
                updated = await _context.Coupons.SingleOrDefaultAsync(coupon => coupon.Id == couponId);
                if (updated == null)
                {
                    return null;
                }
                if (couponModel.Discount != null)
                {
                    updated.Discount = couponModel.Discount;
                }
                if (couponModel.ValidUntilDateTime != null)
                {
                    updated.ValidUntilDateTime = couponModel.ValidUntilDateTime;
                }
                if (couponModel.Status != null)
                {
                    updated.Status = couponModel.Status;
                }
                await _context.SaveChangesAsync();
                return updated;
            }
            return null;

        }
    }
}
