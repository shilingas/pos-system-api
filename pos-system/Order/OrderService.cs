using Microsoft.AspNetCore.Mvc;
using pos_system.Contexts;
using Microsoft.EntityFrameworkCore;

namespace pos_system.Order
{
    public class OrderService : IOrderService
    {
        private readonly PosContext _context;
        public OrderService(PosContext context)
        {
            _context = context;
        }

        public async Task<OrderModel[]> GetAllOrders()
        {
            OrderModel[] orders = new OrderModel[0];
            if (_context != null)
            {
                orders = await _context.Orders.ToArrayAsync();
            }

            return orders;
        }

        public async Task<OrderModel> CreateOrder(string customerId)
        {
            OrderModel order = new OrderModel { 
                Id = Guid.NewGuid().ToString(), 
                CustomerId = customerId, 
                CreatedDateTime = DateTime.Now,
                Status = OrderStatus.Created,
            };
            if (_context != null)
            {
                _context.Add(order);
                await _context.SaveChangesAsync();
            }
            return order;
        }

        public async Task<OrderModel?> GetOrder(string id)
        {
            OrderModel? order = new OrderModel();
            if (_context != null)
            {
                order = await _context.Orders.FindAsync(id);
            }

            if (order != null)
            {
                return order;
            }
            else {
                return null;
            }
            
        }

        public async Task<OrderModel?> UpdateOrder(string orderId, OrderPutRequestModel newOrder) 
        {
            OrderModel? result = new OrderModel();
            if (_context != null)
            {
                result = await _context.Orders.SingleOrDefaultAsync(order => order.Id == orderId); 
                if (result != null)
                {
                    result.Status = newOrder.Status;

                    await _context.SaveChangesAsync();
                    return result;
                }
            }
           
            return null;
        }

        public async Task<bool> DeleteOrder(string id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return false;
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<List<OrderProductModel>> GettAllProducts(string orderId)
        {
            var orderProducts = await _context.OrderProducts.Where(o => o.OrderId == orderId).ToListAsync();
            return orderProducts;
        }

        public async Task<OrderProductModel?> AddProductToOrder(string orderId, OrderProductPostRequestModel newProduct)
        {
            var product = await _context.Products.FindAsync(newProduct.ProductId);
            if (product == null)
            {
                return null;
            }

            var orderProduct = new OrderProductModel
            {
                Id = product.Id,
                Name = product.Name,
                Category = product.Category,
                Price = product.Price,
                Quantity = newProduct.Quantity,
                OrderId = orderId,
            };

            _context.Add(orderProduct);
            await _context.SaveChangesAsync();

            return orderProduct;
        }

        public async Task<OrderProductModel?> GetProductOfAnOrder(string orderId, string productId)
        {
            var orderProduct = await _context.OrderProducts.FirstOrDefaultAsync(o => o.OrderId == orderId && o.Id == productId);
            if (orderProduct == null)
            {
                return null;
            }
            return orderProduct;
        }

        public async Task<OrderProductModel?> UpdateOrderProduct(string orderId, string productId, OrderProductPostRequestModel newOrder)
        {
            var orderProduct = await _context.OrderProducts.FirstOrDefaultAsync(o => o.OrderId == orderId && o.Id == productId);

            if (orderProduct != null)
            {
                orderProduct.Quantity = newOrder.Quantity;

                await _context.SaveChangesAsync();
                return orderProduct;
            }
            return null;
        }

        public async Task<bool> DeleteProductFromOrder(string orderId, string productId)
        {
            var orderProduct = await _context.OrderProducts.FirstOrDefaultAsync(o => o.OrderId == orderId && o.Id == productId);
            if (orderProduct == null)
            {
                return false;
            }

            _context.OrderProducts.Remove(orderProduct);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<List<OrderServiceModel>> GettAllServices(string orderId)
        {
            var orderServices = await _context.OrderServices
                                         .Where(o => o.OrderId == orderId).ToListAsync();

            return orderServices;
        }

        public async Task<OrderServiceModel?> AddServiceToOrder(string orderId, OrderServicePostRequestModel orderServicePostRequest)
        {
            var service = await _context.Services.FindAsync(orderServicePostRequest.ServicetId);
            if (service == null)
            {
                return null;
            }

            var orderService = new OrderServiceModel
            {
                Id = service.Id,
                Name = service.Name,
                Price = service.Price,
                Quantity = orderServicePostRequest.Quantity,
                OrderId = orderId,
            };

            _context.Add(orderService);
            await _context.SaveChangesAsync();

            return orderService;
        }

        public async Task<OrderServiceModel?> GetServiceOfAnOrder(string orderId, string serviceId)
        {
            OrderServiceModel? orderService = await _context.OrderServices.FirstOrDefaultAsync(o => o.OrderId == orderId && o.Id == serviceId);
            if (orderService == null)
            {
                return null;
            }
            return orderService;
        }

        public async Task<OrderServiceModel?> UpdateOrderService(string orderId, string serviceId, OrderServicePostRequestModel newService)
        {
            if (!isValidOrderServicePostRequestModel(newService, serviceId))
            {
                return null;
            }

            OrderServiceModel? orderService = await _context.OrderServices.FirstOrDefaultAsync(o => o.OrderId == orderId && o.Id == serviceId);

            if (orderService != null)
            {
                orderService.Quantity = newService.Quantity;

                await _context.SaveChangesAsync();
                return orderService;
            }
            return null;
        }

        public async Task<bool> DeleteServiceFromOrder(string orderId, string serviceId)
        {
            var orderService = await _context.OrderServices.FirstOrDefaultAsync(o => o.OrderId == orderId && o.Id == serviceId);
            if (orderService == null)
            {
                return false;
            }

            _context.OrderServices.Remove(orderService);
            await _context.SaveChangesAsync();

            return true;
        }

        private bool isValidOrderServicePostRequestModel(OrderServicePostRequestModel orderService, string serviceId) { 
            if (orderService.ServicetId == serviceId)
            {
                return true;
            }
            return false;
        }
    }
}
