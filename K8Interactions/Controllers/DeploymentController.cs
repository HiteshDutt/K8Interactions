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

        [HttpPost("CreateDeployment/{imagePathRelative}")]
        public async Task<IActionResult> CreateDeployment(string imagePathRelative)
        {
            var parsedPath = Uri.UnescapeDataString(imagePathRelative);
            var newDeploymentName = $"threed-{Guid.NewGuid()}".ToLowerInvariant();
            await deployment.CreateDeploymentAsync("3dviz", newDeploymentName, parsedPath);
            return Ok(newDeploymentName);
        }

        [HttpPost("deletedeployment/{deploymentName}")]
        public async Task<IActionResult> DeleteDeployment(string deploymentName)
        {
            await deployment.RemoveDeploymentAsync("3dviz", deploymentName);
            return Ok();
        }
    }
}
