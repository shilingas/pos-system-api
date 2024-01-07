using pos_system.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using pos_system.Order;

namespace pos_system.Services
{
    public class ServiceService : IServiceService
    {
        private readonly PosContext _context;
        public ServiceService(PosContext context)
        {
            _context = context;
        }
        public async Task<ServiceModel[]> GetAllServices()
        {
            ServiceModel[] services = new ServiceModel[0];

            services = await _context.Services.ToArrayAsync();

            return services;

        }

        public async Task<ServiceModel?> CreateService(ServicePostRequestModel servicePostRequest)
        {
            if (!isValidServicePostRequest(servicePostRequest))
            {
                return null;
            }

            ServiceModel service = new ServiceModel
            {
                Id = Guid.NewGuid().ToString(),
                Name = servicePostRequest.Name,
                Duration = servicePostRequest.Duration,
                Price = servicePostRequest.Price,
            };

            _context.Add(service);
            await _context.SaveChangesAsync();

            return service;
        }

        public async Task<ServiceModel?> GetService(string id)
        {
            ServiceModel? service = new ServiceModel();
            service = await _context.Services.FindAsync(id);
            if (service == null)
            {
                return null;
            }
            return service;
        }

        public async Task<ServiceModel?> UpdateService(string serviceId, ServicePutRequestModel servicePutRequest)
        {
            ServiceModel? updatedService = new ServiceModel();
            updatedService = await _context.Services.SingleOrDefaultAsync(service => service.Id == serviceId);
            if (updatedService == null)
            {
                return null;
            }
            if (servicePutRequest.Name != null){
                 updatedService.Name = servicePutRequest.Name;
            }
            if(servicePutRequest.Duration != null)
            {
                updatedService.Duration = servicePutRequest.Duration;
            }
            if(servicePutRequest.Price != null)
            {
                updatedService.Price = servicePutRequest.Price;
            }
            
            await _context.SaveChangesAsync();
            return updatedService;

        }

        public async Task<bool> DeleteService(string serviceId)
        {
            var service = await _context.Services.FindAsync(serviceId);
            if (service == null)
            {
                return false;
            }

            _context.Services.Remove(service);
            await _context.SaveChangesAsync();

            return true;
        }



        private bool isValidServicePostRequest(ServicePostRequestModel service)
        {
            if (String.IsNullOrEmpty(service.Name) || service.Duration == 0 || service.Price == 0) { 
                return false;
            }
            return true;
        }


    }
}
