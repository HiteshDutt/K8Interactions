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

        [HttpPost("CreateService")]
        public async Task<IActionResult> Post([FromBody]ServiceViewModel serviceViewModel)
        {
            var newServiceName = $"{serviceViewModel.ApplicationName}-svc";
            await service.CreateServiceAsync(serviceViewModel.K8Namespace, newServiceName, serviceViewModel.ApplicationName);
            return Ok(newServiceName);
        }

        [HttpDelete("RemoveService")]
        public async Task<IActionResult> Delete([FromBody] ServiceViewModel serviceViewModel)
        {
            await service.RemoveServiceAsync(serviceViewModel.K8Namespace, serviceViewModel.ServiceName);
            return Ok();
        }
    }
}