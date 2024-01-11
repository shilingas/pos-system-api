using Microsoft.AspNetCore.Mvc;
using pos_system.Contexts;
using Microsoft.EntityFrameworkCore;

namespace pos_system.Discounts
{
    public class DiscountService : IDiscountService
    {
        private readonly PosContext _context;
        public DiscountService(PosContext _context)
        {
            this._context = _context;
        }

        public async Task<DiscountModel[]> GetAllDiscounts()
        {
            DiscountModel[] allDiscounts = await _context.Discounts.ToArrayAsync();

            return allDiscounts;
        }

        public async Task<DiscountModel> CreateDiscount(DiscountPostRequestModel discountModel)
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

        public async Task<DiscountModel?> GetDiscountById(String id)
        {
            DiscountModel? discount = await _context.Discounts.FindAsync(id);
            if (discount != null)
            {
                return discount;
            }
            return null;
        }

        public async Task<DiscountModel?> UpdateDiscount(String id, DiscountPostRequestModel discountModel)
        {
            DiscountModel? discount = await _context.Discounts.FindAsync(id);

            if (discount == null)
            {
                return null;
            }
            if (discountModel.ValidUntilDateTime != null)
            {
                discount.ValidUntilDateTime = discountModel.ValidUntilDateTime;
            }
            if (discountModel.Percentage != null)
            {
                discount.Percentage = discountModel.Percentage;
            }
            await _context.SaveChangesAsync();
            return discount;

        }

        public async Task<bool> DeleteDiscount(String id)
        {
            DiscountModel? discount = await _context.Discounts.FindAsync(id);

            if (discount == null)
            {
                return false;
            }

            _context.Discounts.Remove(discount);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<List<DiscountProductModel>?> GetDiscountedProducts(String id)
        {
            List<DiscountProductModel>? products = await _context.DiscountProducts.Where(p => p.DiscountId == id).ToListAsync();

            return products;
        }

        public async Task<DiscountProductModel?> AddProductToDiscount(String id, [FromBody] DiscountProductPostRequestModel discountRequestModel)
        {
            var existingProduct = await _context.Products.FindAsync(discountRequestModel.ProductId);
            var existingDiscount = await _context.Discounts.FindAsync(id);

            if (existingProduct == null || existingDiscount == null)
            {
                return null;
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

        public async Task<bool> DeleteDiscountOnProduct(String discountId, String productId)
        {
            DiscountProductModel? discountProduct = await _context.DiscountProducts.FirstOrDefaultAsync(dp => dp.ProductId == productId && dp.DiscountId == discountId);

            if (discountProduct == null)
            {
                return false;
            }

            _context.Remove(discountProduct);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<List<DiscountServiceModel>?> GetDiscountedServices(String id)
        {
            List<DiscountServiceModel>? services = new List<DiscountServiceModel>();
            services = await _context.DiscountServices.Where(s => s.DiscountId == id).ToListAsync();

            return services;
        }

        public async Task<DiscountServiceModel?> AddServiceToDiscount(String id, [FromBody] DiscountServicePostRequestModel discountRequestModel)
        {
            var existingService = await _context.Services.FindAsync(discountRequestModel.ServiceId);
            var existingDiscount = await _context.Discounts.FindAsync(id);

            if (existingService == null || existingDiscount == null)
            {
                return null;
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

        public async Task<bool> DeleteDiscountOnService(String discountId, String serviceId)
        {
            DiscountServiceModel? discountService = new DiscountServiceModel();
            discountService = await _context.DiscountServices.FirstOrDefaultAsync(ds => ds.ServiceId == serviceId && ds.DiscountId == discountId);

            if (discountService == null)
            {
                return false;
            }

            _context.Remove(discountService);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}