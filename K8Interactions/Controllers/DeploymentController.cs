using System.Text.Encodings.Web;
using Handlers;
using Microsoft.AspNetCore.Mvc;

namespace K8Interactions.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class DeploymentController : ControllerBase
    {
        private readonly IDeployment deployment;
        public DeploymentController(IDeployment deployment)
        {
            this.deployment = deployment;
        }

        [HttpPost("CreateDeployment")]
        public async Task<IActionResult> CreateDeployment([FromBody]DeploymentViewModel input)
        {
            var parsedPath = Uri.UnescapeDataString(input.RelativeImagePath);
            var newDeploymentName = $"threed-{Guid.NewGuid()}".ToLowerInvariant();
            await deployment.CreateDeploymentAsync(input.K8Namespace, newDeploymentName, parsedPath);
            return Ok(newDeploymentName);
        }

        [HttpDelete("deletedeployment")]
        public async Task<IActionResult> DeleteDeployment([FromBody] DeploymentViewModel input)
        {
            await deployment.RemoveDeploymentAsync(input.K8Namespace, input.DeploymentName);
            return Ok();
        }
    }
}
