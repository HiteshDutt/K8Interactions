using Handlers;
using Microsoft.AspNetCore.Mvc;

namespace K8Interactions.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IngressController : ControllerBase
    {
        private readonly IIngress ingress;
        private readonly ILogger<IngressController> _logger;
        public IngressController(IIngress ingress, ILogger<IngressController> logger)
        {
            this.ingress = ingress;
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IActionResult Get()
        {
            return Ok(ingress.GetIngressAsync("default", "threed-ingress"));
        }

        [HttpPost("AddRule/{serviceName}")]
        public async Task<IActionResult> Post(string serviceName)
        {
            var newRuleName = $"threed-{Guid.NewGuid()}";
            var value = await ingress.AddRuleAsync("default", "threed-ingress", newRuleName, serviceName);
            return Ok(value);
        }

        [HttpDelete("RemoveRule/{ruleName}")]
        public async Task<IActionResult> Delete(string ruleName)
        {
            await ingress.RemoveRuleAsync("default", "threed-ingress", ruleName);
            return Ok();
        }
    }
}