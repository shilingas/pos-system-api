namespace pos_system.ProductService.Services
{
    public interface IServiceService
    {
        Task<ServiceModel[]> GetAllServices();
        Task<ServiceModel?> CreateService(ServicePostRequestModel servicePostRequest);
        Task<ServiceModel?> GetService(string id);
        Task<ServiceModel?> UpdateService(string serviceId, ServicePutRequestModel servicePostRequest);
        Task<bool> DeleteService(string serviceId);
    }
}