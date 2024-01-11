using Microsoft.AspNetCore.Mvc;
using pos_system.Contexts;

namespace pos_system.Order.Tips
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipController : ControllerBase
    {
        private readonly ITipService _tipService;
        public TipController(ITipService tipService)
        {
            _tipService = tipService;
        }

        [HttpPut("{orderId}")]
        public async Task<ActionResult<OrderModel>> AddTip(string orderId, [FromBody] TipRequestModel newOrder)
        {
            OrderModel? order = await _tipService.AddTip(orderId, newOrder);
            if (order != null)
            {
                return Ok(order);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
