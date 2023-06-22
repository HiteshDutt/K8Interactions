namespace Handlers
{
    public class DeploymentViewModel
    {
        public string RelativeImagePath { get; set; } = string.Empty;
        public string K8Namespace { get; set; } = string.Empty;
        public string DeploymentName { get; set; } = string.Empty;
    }
}
