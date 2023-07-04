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

        [HttpPost]
        public async Task<IActionResult> CreateService([FromBody]ServiceViewModel serviceViewModel)
        {
            var newServiceName = $"{serviceViewModel.ApplicationName}-svc";
            await service.CreateServiceAsync(serviceViewModel.K8Namespace, newServiceName, serviceViewModel.ApplicationName);
            return Ok(newServiceName);
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveService([FromBody] ServiceViewModel serviceViewModel)
        {
            await service.RemoveServiceAsync(serviceViewModel.K8Namespace, serviceViewModel.ServiceName);
            return Ok();
        }
    }
}