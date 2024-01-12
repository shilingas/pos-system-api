using Microsoft.EntityFrameworkCore;
using pos_system.Contexts;
using pos_system.Customers.Coupons;

namespace pos_system.Customers
{
    public class CustomerService : ICustomerService
    {
        private readonly PosContext _context;
        public CustomerService(PosContext context)
        {
            _context = context;
        }
        public async Task<CustomerModel?> CreateCustomer(CustomerPostRequestModel customerModel)
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

        public async Task<CustomerModel[]> GetAllCustomers()
        {

            CustomerModel[] customers = new CustomerModel[0];
            if (_context != null)
            {
                customers = await _context.Customers.ToArrayAsync();
            }

            return customers;
        }

        public async Task<CustomerModel?> GetCustomer(string id)
        {
            CustomerModel? customer = new CustomerModel();
            customer = await _context.Customers.FindAsync(id);
            if (customer != null)
            {
                return customer;
            }
            else
            {
                return null;
            }
        }

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
