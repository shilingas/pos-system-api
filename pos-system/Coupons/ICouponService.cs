using pos_system.Services;

namespace pos_system.Coupons
{
    public interface ICouponService
    {
        Task<CouponModel[]> GetAllCoupons();
        Task<CouponModel?> CreateCoupon(CouponPostRequestModel couponModel);
        Task<CouponModel?> GetCoupon(string id);
        Task<CouponModel?> UpdateCoupon(string couponId, CouponPostRequestModel couponModel);
        Task<bool> DeleteCoupon(string id);
    }
}
