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

        [HttpGet("GetIngress/{ingressName}/{k8Namespace}")]
        public IActionResult Get(string ingressName, string k8Namespace)
        {
            return Ok(ingress.GetIngressAsync(k8Namespace, ingressName));
        }

        [HttpPost("AddRule")]
        public async Task<IActionResult> Post([FromBody] IngressViewModel ingressViewModel)
        {
            var newRuleName = $"threed-{Guid.NewGuid()}";
            var value = await ingress.AddRuleAsync(ingressViewModel.K8Namespace, ingressViewModel.IngressName, newRuleName, ingressViewModel.ServiceName);
            return Ok(value);
        }

        [HttpDelete("RemoveRule")]
        public async Task<IActionResult> Delete([FromBody] IngressViewModel ingressViewModel)
        {
            await ingress.RemoveRuleAsync(ingressViewModel.K8Namespace, ingressViewModel.IngressName, ingressViewModel.RuleName);
            return Ok();
        }
    }
}