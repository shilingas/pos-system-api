using Microsoft.AspNetCore.Mvc;
using pos_system.Contexts;
using pos_system.Order;
using pos_system.Products;

namespace pos_system.Payments
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpGet]
        public async Task<ActionResult<PaymentModel[]>> GetPayments()
        {
            PaymentModel[] payments = await _paymentService.GetPayments();
            return Ok(payments);
        }

        [HttpPost]
        public async Task<ActionResult<PaymentModel>> CreatePayment([FromBody] PaymentPostRequest paymentPostRequest)
        {
            PaymentModel? payment = await _paymentService.CreatePayment(paymentPostRequest);
            if (payment == null)
            {
                return BadRequest();
            }
            return Ok(payment);
        }

        [HttpGet("customer/{customerId}")]
        public async Task<ActionResult<List<PaymentModel>>> GetUsersPayments (string customerId)
        {
            List<PaymentModel>? usersPayments = await _paymentService.GetAllUserPayments(customerId);
            return Ok(usersPayments);
        }

        [HttpGet("order/{orderId}")]
        public async Task<ActionResult<PaymentModel>> GetOrderPayment (string orderId)
        {
            PaymentModel? payment = await _paymentService.GetOrderPayment(orderId);
            return payment == null ? BadRequest() : Ok(payment);
        }

        [HttpGet("{paymentId}")]
        public async Task<ActionResult<PaymentModel>> GetPayment (string paymentId)
        {
            PaymentModel? payment = await _paymentService.GetPayment(paymentId);
            return payment == null ? BadRequest() : Ok(payment);
        }

        [HttpPut("{paymentId}")]
        public async Task<ActionResult<PaymentModel>> UpdatePayment (string paymentId, [FromBody] PaymentPostRequest paymentPostRequest)
        {
            PaymentModel payment = await _paymentService.UpdatePayment(paymentId, paymentPostRequest);
            if (payment == null)
            {
                return BadRequest();
            }
            return Ok(payment);
        }

        [HttpDelete("{paymentId}")]
        public async Task<IActionResult> DeletePayment (string paymentId)
        {
            bool isDeleted = await _paymentService.DeletePayment(paymentId);
            if (!isDeleted) { 
                return NotFound();
            }
            return NoContent();
        }

    }
}
