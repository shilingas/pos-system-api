using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pos_system.Contexts;
using pos_system.Coupons;
using pos_system.Services;

namespace pos_system.Customers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : Controller
    {
        private readonly PosContext _context;
        private readonly ICustomerService _customerService;
        public CustomerController(PosContext _context, ICustomerService customerService)
        {
            this._context = _context;
            _customerService = customerService;
        }
        [HttpPost]
        [Produces("application/json")]
        public async Task<ActionResult<CustomerModel>> CreateCustomer([FromBody] CustomerPostRequestModel customerModel)
        {
            CustomerModel? coupon = await _customerService.CreateCustomer(customerModel);
            if (coupon == null)
            {
                return BadRequest();
            }
            return Ok(coupon);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerModel>?> GetCustomer(string id)
        {
            CustomerModel? customer = await _customerService.GetCustomer(id);
            if (customer != null)
            {
                return customer;
            }
            else
            {
                return NotFound();
            }
        }
        [HttpGet]
        public async Task<CustomerModel[]> GetAllCustomers()
        {
            return await _customerService.GetAllCustomers();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCustomer(string id)
        {
            bool isDeleted = await _customerService.DeleteCustomer(id);
            if (isDeleted)
            {
                return NoContent();
            }
            return NotFound();
        }
        [HttpPut("{customerId}")]
        public async Task<ActionResult<CustomerModel>> UpdateCustomer(string customerId, CustomerPostRequestModel customerModel)
        {
            CustomerModel? customer = await _customerService.UpdateCustomer(customerId, customerModel);
            if (customer != null)
            {
                return Ok(customer);
            }
            else
            {
                return NotFound();
            }

        }
    }
}
