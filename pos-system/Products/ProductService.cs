using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pos_system.Contexts;
using pos_system.Customers;

namespace pos_system.Products
{
    public class ProductService : IProductService
    {
        private readonly PosContext _context;
        public ProductService(PosContext context)
        {
            _context = context;
        }
        public async Task<ProductModel?> CreateProduct(ProductPostRequestModel productModel)
        {
            ProductModel product = new ProductModel
            {
                Id = Guid.NewGuid().ToString(),
                Name = productModel.Name,
                Price = productModel.Price,
                CategoryType = productModel.CategoryType,
            };
            _context.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<ProductModel[]> GetAllProducts()
        {
            ProductModel[] products = new ProductModel[0];
            if (_context != null)
            {
                products = await _context.Products.ToArrayAsync();
            }

            return products;
        }

        public async Task<ProductModel?> GetProduct(string id)
        {
            ProductModel? product = new ProductModel();
            product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                return product;
            }
            else
            {
                return null;
            }
        }
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

        public async Task<ProductModel?> UpdateProduct(string productId, ProductPostRequestModel productModel)
        {
            ProductModel? updated = new ProductModel();
            if (_context != null)
            {
                updated = await _context.Products.SingleOrDefaultAsync(prod => prod.Id == productId);
                if (updated == null)
                {
                    return null;
                }
                if (productModel.Name != null)
                {
                    updated.Name = productModel.Name;
                }
                if (productModel.Price != null)
                {
                    updated.Price = productModel.Price;
                }
                if (productModel.CategoryType != null)
                {
                    updated.CategoryType = productModel.CategoryType;
                }
                await _context.SaveChangesAsync();
                return updated;
            }
            return null;
        }
        public async Task<ProductModel[]?> GetProductsByCategory(string categoryType)
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
