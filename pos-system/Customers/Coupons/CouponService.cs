using Microsoft.AspNetCore.Mvc;
using pos_system.Contexts;
using Microsoft.EntityFrameworkCore;
namespace pos_system.Customers.Coupons
{
    public class CouponService : ICouponService
    {
        private readonly PosContext _context;

        public CouponService(PosContext context)
        {
            _context = context;
        }
        public async Task<CouponModel?> CreateCoupon(CouponPostRequestModel couponModel)
        {
            CouponModel coupon = new CouponModel
            {
                Id = Guid.NewGuid().ToString(),
                Discount = couponModel.Discount,
                ValidUntilDateTime = couponModel.ValidUntilDateTime,
                Status = couponModel.Status,
            };
            _context.Add(coupon);
            await _context.SaveChangesAsync();
            return coupon;
        }

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

        public async Task<CouponModel[]> GetAllCoupons()
        {
            CouponModel[] coupons = new CouponModel[0];
            if (_context != null)
            {
                coupons = await _context.Coupons.ToArrayAsync();
            }

            return coupons;
        }

        public async Task<CouponModel?> GetCoupon(string id)
        {
            CouponModel? coupon = new CouponModel();
            coupon = await _context.Coupons.FindAsync(id);
            if (coupon != null)
            {
                return coupon;
            }
            else
            {
                return null;
            }
        }

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
