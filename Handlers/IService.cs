namespace Handlers
{
    public interface IService
    {
        Task CreateServiceAsync(string k8Namespace, string serviceName, string appName);
        Task RemoveServiceAsync(string k8Namespace, string serviceName);
    }
}
