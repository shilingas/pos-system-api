using Microsoft.AspNetCore.Mvc;
using pos_system.Contexts;
using pos_system.Order;

namespace pos_system.ProductService.Services
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        private readonly IServiceService _serviceService;
        public ServiceController(IServiceService serviceService)
        {
            _serviceService = serviceService;
        }

        [HttpGet]
        public async Task<ServiceModel[]> GetAllServices()
        {
            return await _serviceService.GetAllServices();
        }

        [HttpPost]
        [Produces("application/json")]
        public async Task<ActionResult<ServiceModel>> CreateService([FromBody] ServicePostRequestModel servicePostRequest)
        {
            ServiceModel? createdService = await _serviceService.CreateService(servicePostRequest);
            if (createdService == null)
            {
                return BadRequest();
            }
            return Ok(createdService);

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceModel>> GetService(string id)
        {

            ServiceModel? service = await _serviceService.GetService(id);
            if (service != null)
            {
                return service;
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPut("{serviceId}")]
        [Produces("application/json")]
        public async Task<ActionResult<ServiceModel>> UpdateService(string serviceId, [FromBody] ServicePutRequestModel serviceRequest)
        {
            ServiceModel? updatedService = await _serviceService.UpdateService(serviceId, serviceRequest);
            if (updatedService != null)
            {
                return Ok(updatedService);
            }
            else
            {
                return NotFound();
            }

        }

        [HttpDelete("{serviceId}")]
        public async Task<IActionResult> DeleteService(string serviceId)
        {
            bool isDeleted = await _serviceService.DeleteService(serviceId);
            if (isDeleted)
            {
                return NoContent();
            }
            return NotFound();

        }

    }
}
