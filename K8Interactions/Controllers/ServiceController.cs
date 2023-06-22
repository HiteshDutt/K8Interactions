using Handlers;
using Microsoft.AspNetCore.Mvc;

namespace K8Interactions.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ServiceController : ControllerBase
    {
        private readonly IService service;
        private readonly ILogger<ServiceController> _logger;
        public ServiceController(IService service, ILogger<ServiceController> logger)
        {
            this.service = service;
            _logger = logger;
        }

        [HttpPost("CreateService/{appName}")]
        public async Task<IActionResult> Post(string appName)
        {
            var newServiceName = $"{appName}-svc";
            await service.CreateServiceAsync("3dviz", newServiceName, appName);
            return Ok(newServiceName);
        }

        [HttpDelete("RemoveService/{serviceName}")]
        public async Task<IActionResult> Delete(string serviceName)
        {
            await service.RemoveServiceAsync("3dviz", serviceName);
            return Ok();
        }
    }
}