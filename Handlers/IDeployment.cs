namespace Handlers
{
    public interface IDeployment
    {
        Task CreateDeploymentAsync(string k8Namespace, string appName, string threeDImageRelativePath);
        Task RemoveDeploymentAsync(string k8Namespace, string appName);
    }
}
