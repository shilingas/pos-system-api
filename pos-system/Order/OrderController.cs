using Microsoft.AspNetCore.Mvc;
using pos_system.Contexts;
using Microsoft.EntityFrameworkCore;

namespace pos_system.Order
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly PosContext _context;
        private readonly IOrderService _orderService;
        public OrderController(PosContext context, IOrderService orderService) {
            _context = context;
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<OrderModel[]> GetAllOrders()
        {
            return await _orderService.GetAllOrders();
        }

        [HttpPost]
        [Produces("application/json")]
        public async Task<IActionResult> CreateOrder([FromBody] string customerId)
        {
            if (!String.IsNullOrEmpty(customerId)) { 
                return Ok(await _orderService.CreateOrder(customerId));
            }else
            {
                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderModel>> GetOrder(string id)
        {

            OrderModel? order = await _orderService.GetOrder(id);
            if (order != null)
            {
                return order;
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPut("{id}")]
        [Produces("application/json")]
        public async Task<IActionResult> UpdateOrder(string id, [FromBody] OrderPutRequestModel newOrder)
        {
            OrderModel? order = await _orderService.UpdateOrder(id, newOrder);
            if (order != null)
            {
                return Ok(order);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(string id)
        {
            bool isDeleted = await _orderService.DeleteOrder(id);
            if (isDeleted)
            {
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("{orderId}/products")]
        public async Task<List<OrderProductModel>> GettAllProducts(string orderId)
        {
            return await _orderService.GettAllProducts(orderId);
        }

        [HttpPost("{orderId}/products")]
        [Produces("application/json")]
        public async Task<IActionResult> AddProductToOrder(string orderId, [FromBody] OrderProductPostRequestModel newProduct)
        {
            OrderProductModel? orderProduct = await _orderService.AddProductToOrder(orderId, newProduct);
            if (orderProduct != null)
            {
                return Ok(orderProduct);
            }
            else
            {
                return NotFound();
            }
            
        }


        [HttpGet("{orderId}/products/{productId}")]
        public async Task<ActionResult<OrderProductModel>> GetProductOfAnOrder(string orderId, string productId)
        {
            OrderProductModel? orderProduct = await _orderService.GetProductOfAnOrder(orderId, productId);
            if (orderProduct == null)
            {
                return NotFound();
            }
            return orderProduct;
        }

        [HttpPut("{orderId}/products/{productId}")]
        [Produces("application/json")]
        public async Task<IActionResult> UpdateOrderProduct(string orderId, string productId, [FromBody] OrderProductPostRequestModel newOrder)
        {
            OrderProductModel? orderProduct = await _orderService.UpdateOrderProduct(orderId, productId, newOrder);
            if (orderProduct != null)
            {
                return Ok(orderProduct);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete("{orderId}/products/{productId}")]
        public async Task<IActionResult> DeleteProductFromOrder(string orderId, string productId)
        {
            if (await _orderService.DeleteProductFromOrder(orderId, productId))
            {
                return NoContent();
            }
            else{
                return NotFound();
            }
        }

        [HttpGet("{orderId}/services")]
        public async Task<List<OrderServiceModel>> GettAllServices(string orderId)
        {
            return await _orderService.GettAllServices(orderId);
        }

        [HttpPost("{orderId}/services")]
        [Produces("application/json")]
        public async Task<ActionResult<OrderServiceModel>> AddServuceToOrder(string orderId, [FromBody] OrderServicePostRequestModel newService)
        {
            OrderServiceModel? orderService = await _orderService.AddServiceToOrder(orderId, newService);
            if (orderService != null)
            {
                return Ok(orderService);
            }
            else
            {
                return NotFound();
            }

        }

        [HttpGet("{orderId}/services/{serviceId}")]
        public async Task<ActionResult<OrderServiceModel>> GetServiceOfAnOrder(string orderId, string serviceId)
        {
            OrderServiceModel? orderService = await _orderService.GetServiceOfAnOrder(orderId, serviceId);
            if (orderService == null)
            {
                return NotFound();
            }
            return orderService;
        }

        [HttpPut("{orderId}/services/{serviceId}")]
        [Produces("application/json")]
        public async Task<ActionResult<OrderServiceModel>> UpdateOrderService(string orderId, string serviceId, [FromBody] OrderServicePostRequestModel newService)
        {
            OrderServiceModel? orderService = await _orderService.UpdateOrderService(orderId, serviceId, newService);
            if (orderService != null)
            {
                return Ok(orderService);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete("{orderId}/services/{serviceId}")]
        public async Task<IActionResult> DeleteServiceFromOrder(string orderId, string serviceId)
        {
            if (await _orderService.DeleteServiceFromOrder(orderId, serviceId))
            {
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }


    }
}
