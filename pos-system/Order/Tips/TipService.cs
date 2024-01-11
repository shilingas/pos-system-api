using pos_system.Contexts;
using Microsoft.EntityFrameworkCore;

namespace pos_system.Order.Tips
{
    public class TipService : ITipService
    {
        private readonly PosContext _context;
        public TipService(PosContext context)
        {
            _context = context;
        }
        public async Task<OrderModel?> AddTip(string orderId, TipRequestModel tip)
        {
            OrderModel? result = new OrderModel();
            if (_context != null)
            {
                result = await _context.Orders.SingleOrDefaultAsync(order => order.Id == orderId);
                if (result != null)
                {
                    result.Tip = tip.Tip;

                    await _context.SaveChangesAsync();
                    return result;
                }
            }

            return null;
        }
    }
}
