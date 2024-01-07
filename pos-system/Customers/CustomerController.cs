using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pos_system.Contexts;

namespace pos_system.Customers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : Controller
    {
        private readonly PosContext _context;
        public CustomerController(PosContext _context)
        {
            this._context = _context;
        }
        [HttpPost]
        [Produces("application/json")]
        public async Task<ActionResult<CustomerModel>> CreateCustomer([FromBody] CustomerPostRequestModel customerModel)
        {
            CustomerModel customer = new CustomerModel
            {
                Id = Guid.NewGuid().ToString(),
                Name = customerModel.Name,
                Email = customerModel.Email,
                Phone = customerModel.Phone,
            };
            _context.Add(customer);
            await _context.SaveChangesAsync();
            return customer;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerModel>?> GetCustomer(string id)
        {
            CustomerModel? customer = new CustomerModel();
            customer = await _context.Customers.FindAsync(id);
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
            CustomerModel[] customers = new CustomerModel[0];
            if (_context != null)
            {
                customers = await _context.Customers.ToArrayAsync();
            }

            return customers;
        }
        [HttpDelete("{id}")]
        public async Task<bool> DeleteCustomer(string id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return false;
            }

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            return true;
        }
        [HttpPut("{customerId}")]
        public async Task<CustomerModel?> UpdateCustomer(string customerId, CustomerPostRequestModel customerModel)
        {
            CustomerModel? updated = new CustomerModel();
            if (_context != null)
            {
                updated = await _context.Customers.SingleOrDefaultAsync(customer => customer.Id == customerId);
                if (updated == null)
                {
                    return null;
                }
                if (customerModel.Name != null)
                {
                    updated.Name = customerModel.Name;
                }
                if (customerModel.Email != null)
                {
                    updated.Email = customerModel.Email;
                }
                if (customerModel.Phone != null)
                {
                    updated.Phone = customerModel.Phone;
                }
                await _context.SaveChangesAsync();
                return updated;
            }
            return null;

        }
    }
}
