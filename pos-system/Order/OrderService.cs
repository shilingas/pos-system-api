using Microsoft.AspNetCore.Mvc;
using pos_system.Contexts;
using Microsoft.EntityFrameworkCore;

namespace pos_system.Order
{
    public class OrderService
    {
        private readonly PosContext? _context;
        public OrderService(PosContext? context)
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
            OrderModel order = new OrderModel { Id = Guid.NewGuid().ToString(), CustomerId = customerId };
            if (_context != null)
            {
                _context.Add(order);
                await _context.SaveChangesAsync();
            }
            return order;
        }

        public async Task<OrderModel> GetOrder(string id)
        {
            OrderModel order = new OrderModel();
            if (_context != null)
            {
                order = await _context.Orders.FindAsync(id);
            }
            
            return order;
        }

        public async Task<IActionResult> UpdateOrder(string id, OrderRequestModel newOrder) //vartotojo inputo validacija
        {
            OrderModel result = new OrderModel();
            if (_context == null)
            {
                result = await _context.Orders.SingleOrDefaultAsync(order => order.Id == id);
            }

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
    }
}
