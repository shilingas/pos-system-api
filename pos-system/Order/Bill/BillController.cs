using Microsoft.AspNetCore.Mvc;
using pos_system.Contexts;
using pos_system.Order;

namespace pos_system.Order.Bill
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillController
    {
        private readonly IBillService _billService;
        public BillController(IBillService billService)
        {
            _billService = billService;
        }
        [HttpGet("{orderId}")]
        public async Task<BillModel> getBill(string orderId)
        {
            return await _billService.getBill(orderId);
        }

    }
}
