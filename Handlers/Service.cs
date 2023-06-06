using k8s;
using k8s.Models;

namespace Handlers
{
    internal class Service : IService
    {
        private readonly Kubernetes client;
        public Service(KubernetesClientConfiguration config)
        {
            client = new Kubernetes(config);
        }

        public Task CreateServiceAsync(string k8Namespace, string serviceName, string appName)
        {
            var service = new V1Service
            {
                ApiVersion = "v1",
                Kind = "Service",
                Metadata = new V1ObjectMeta
                {
                    Name = serviceName,
                },
                Spec = new V1ServiceSpec
                {
                    Ports = new List<V1ServicePort>
                    {
                        new V1ServicePort
                        {
                            Port= 80,
                            TargetPort=80,
                            Protocol= "TCP",
                        }
                    },
                    Selector= new Dictionary<string, string>
                    {
                        { "app", appName }
                    },
                    Type = "ClusterIP"
                }
            };
            return client.CreateNamespacedServiceAsync(service, k8Namespace);
        }

        public Task RemoveServiceAsync(string k8Namespace, string serviceName)
        {
            return client.DeleteNamespacedServiceAsync(serviceName, k8Namespace);
        }
    }
}
