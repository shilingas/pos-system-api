using Microsoft.AspNetCore.Mvc;
using pos_system.Contexts;
using pos_system.Models;
using Microsoft.EntityFrameworkCore;
using System.Data.Entity.Core.Mapping;

namespace pos_system.Order
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly PosContext? _context;
        private readonly OrderService _orderService;
        public OrderController(PosContext? context, OrderService orderService) {
            _context = context;
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<OrderModel[]> GetAllOrders()
        {
            var orders = await _context.Orders.ToArrayAsync();
            return orders;
            //return await _orderService.GetAllOrders();
        }

        [HttpPost]
        [Produces("application/json")]
        public async Task<IActionResult> CreateOrder([FromBody] string customerId)
        {
            var order = new OrderModel { Id = Guid.NewGuid().ToString(), CustomerId = customerId };
            _context.Add(order);
            await _context.SaveChangesAsync();
            return Ok(_orderService.CreateOrder(customerId));
        }

        [HttpGet("{id}")]
        public async Task<OrderModel> GetOrder(string id)
        {
            var order = await _context.Orders.FindAsync(id);
            return order;
            //return await _orderService.GetOrder(id);
        }

        [HttpPut("{id}")]
        [Produces("application/json")]
        public async Task<IActionResult> UpdateOrder(string id, [FromBody] OrderRequestModel newOrder)
        {
            var result = await _context.Orders.SingleOrDefaultAsync(order => order.Id == id);
            
            if (result != null)
            {
                result.Id = newOrder.Id;
                result.CustomerId = newOrder.CustomerId;
                result.CreatedDateTime = newOrder.CreatedDateTime;
                result.Status = newOrder.Status;

                await _context.SaveChangesAsync();
                return Ok(result);
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(string id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("{orderId}/products")]
        public async Task<List<OrderProductModel>> gettAllProducts(string orderId)
        {
            var orderProducts = await _context.OrderProducts.Where(o => o.OrderId == orderId).ToListAsync();
            return orderProducts; 
        }

        [HttpPost("{orderId}/products")]
        [Produces("application/json")]
        public async Task<IActionResult> AddProductToOrder(string orderId, [FromBody] OrderProductPostRequestModel newProduct)
        {
            //var order = await _context.Orders.Include(o => o.Products).SingleOrDefaultAsync(o => o.Id == orderId); ;
            var product = await _context.Products.FindAsync(newProduct.ProductId);

            var orderProduct = new OrderProductModel { 
                Id = product.Id, 
                Name = product.Name, 
                Category = product.Category, 
                Price = product.Price, 
                Quantity = newProduct.Quantity,
                OrderId = orderId,
            };

            _context.Add(orderProduct);
            await _context.SaveChangesAsync();

            return Ok(orderProduct);
        }


        [HttpGet("{orderId}/products/{productId}")]
        public async Task<OrderProductModel> getProductOfAnOrder(string orderId, string productId)
        {
            var orderProduct = await _context.OrderProducts.FirstOrDefaultAsync(o => o.OrderId == orderId && o.Id == productId);
            return orderProduct;
        }

        [HttpPut("{orderId}/products/{productId}")]
        [Produces("application/json")]
        public async Task<IActionResult> UpdateOrderProduct(string orderId, string productId, [FromBody] OrderProductPostRequestModel newOrder)
        {
            var orderProduct = await _context.OrderProducts.FirstOrDefaultAsync(o => o.OrderId == orderId && o.Id == productId);

            if (orderProduct != null)
            {
                orderProduct.Quantity = newOrder.Quantity;

                await _context.SaveChangesAsync();
                return Ok(orderProduct);
            }
            return BadRequest();
        }

        [HttpDelete("{orderId}/products/{productId}")]
        public async Task<IActionResult> DeleteProductFromOrder(string orderId, string productId)
        {
            var orderProduct = await _context.OrderProducts.FirstOrDefaultAsync(o => o.OrderId == orderId && o.Id == productId);
            if (orderProduct == null)
            {
                return NotFound();
            }

            _context.OrderProducts.Remove(orderProduct);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
