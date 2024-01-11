namespace pos_system.Customers
{
    public interface ICustomerService
    {
        Task<CustomerModel[]> GetAllCustomers();
        Task<CustomerModel?> CreateCustomer(CustomerPostRequestModel customerModel);
        Task<CustomerModel?> GetCustomer(string id);
        Task<CustomerModel?> UpdateCustomer(string customerId, CustomerPostRequestModel customerModel);
        Task<bool> DeleteCustomer(string id);
    }
}
