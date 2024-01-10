using pos_system.Customers;

namespace pos_system.Products
{
    public interface IProductService
    {
        Task<ProductModel[]> GetAllProducts();
        Task<ProductModel?> CreateProduct(ProductPostRequestModel productModel);
        Task<ProductModel?> GetProduct(string id);
        Task<ProductModel?> UpdateProduct(string productId, ProductPostRequestModel productModel);
        Task<bool> DeleteProduct(string id);
        Task<ProductModel[]> GetProductsByCategory(string categoryType);
    }
}
