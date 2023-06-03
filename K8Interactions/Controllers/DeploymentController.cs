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

        [HttpPost("CreateDeployment/{imagePathRelative}")]
        public async Task<IActionResult> CreateDeployment(string imagePathRelative)
        {
            var newDeploymentName = $"threed-{Guid.NewGuid()}".ToLowerInvariant();
            await deployment.CreateDeploymentAsync("default", newDeploymentName, imagePathRelative);
            return Ok(newDeploymentName);
        }

        [HttpPost("deletedeployment/{deploymentName}")]
        public async Task<IActionResult> DeleteDeployment(string deploymentName)
        {
            await deployment.RemoveDeploymentAsync("default", deploymentName);
            return Ok();
        }
    }
}
