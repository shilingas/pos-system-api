
using pos_system.ProductService.Products;

namespace pos_system.Order
{
    public interface IOrderService
    {
        Task<OrderProductModel?> AddProductToOrder(string orderId, OrderProductPostRequestModel newProduct);
        Task<OrderServiceModel?> AddServiceToOrder(string orderId, OrderServicePostRequestModel orderServicePostRequest);
        Task<OrderModel> CreateOrder(string customerId);
        Task<bool> DeleteOrder(string id);
        Task<bool> DeleteProductFromOrder(string orderId, string productId);
        Task<bool> DeleteServiceFromOrder(string orderId, string serviceId);
        Task<OrderModel[]> GetAllOrders();
        Task<List<OrderProductModel?>> GetAllProducts(string orderId);
        Task<List<OrderServiceModel>> GetAllServices(string orderId);
        Task<OrderModel?> GetOrder(string id);
        Task<OrderProductModel?> GetProductOfAnOrder(string orderId, string productId);
        Task<OrderServiceModel?> GetServiceOfAnOrder(string orderId, string serviceId);
        Task<OrderModel?> UpdateOrder(string id, OrderPutRequestModel newOrder);
        Task<OrderProductModel?> UpdateOrderProduct(string orderId, string productId, OrderProductPostRequestModel newOrder);
        Task<OrderServiceModel?> UpdateOrderService(string orderId, string serviceId, OrderServicePostRequestModel newService);
    }
}