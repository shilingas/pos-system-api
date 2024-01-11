using Microsoft.AspNetCore.Mvc;

namespace pos_system.Discounts
{
    public interface IDiscountService
    {
        Task<DiscountProductModel?> AddProductToDiscount(string id, DiscountProductPostRequestModel discountRequestModel);
        Task<DiscountServiceModel?> AddServiceToDiscount(string id, DiscountServicePostRequestModel discountRequestModel);
        Task<DiscountModel> CreateDiscount(DiscountPostRequestModel discountModel);
        Task<bool> DeleteDiscount(string id);
        Task<bool> DeleteDiscountOnProduct(string discountId, string productId);
        Task<bool> DeleteDiscountOnService(string discountId, string serviceId);
        Task<DiscountModel[]> GetAllDiscounts();
        Task<DiscountModel?> GetDiscountById(string id);
        Task<List<DiscountProductModel>?> GetDiscountedProducts(string id);
        Task<List<DiscountServiceModel>?> GetDiscountedServices(string id);
        Task<DiscountModel?> UpdateDiscount(string id, DiscountPostRequestModel discountModel);
    }
}