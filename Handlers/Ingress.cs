using k8s;
using k8s.Models;

namespace Handlers
{
    internal class Ingress : IIngress
    {
        private readonly Kubernetes client;

        public Ingress()
        {
            var config = KubernetesClientConfiguration.BuildConfigFromConfigFile();
            client = new Kubernetes(config);
        }

        public string GetIngressAsync(string k8Namespace, string ingressName)
        {
            var networking = client.NetworkingV1.ListNamespacedIngress(k8Namespace);
            var ingress = networking.Items.FirstOrDefault(x=>x.Metadata.Name.ToUpperInvariant() == ingressName.ToUpperInvariant());
            return ingress.Spec.Rules.ToString();
        }

        public async Task<string> AddRuleAsync(string k8Namespace, string ingressName,string ruleName, string serviceName)
        {
            var networking = await client.NetworkingV1.ListNamespacedIngressAsync(k8Namespace);
            var ingress = networking.Items.FirstOrDefault(x => x.Metadata.Name.ToUpperInvariant() == ingressName.ToUpperInvariant());
            if(ingress == null)
            {
                throw new FileNotFoundException($"Ingress {ingressName} not found");
            }

            foreach (var rule in ingress.Spec.Rules)
            {

                rule.Http.Paths.Insert(0, new k8s.Models.V1HTTPIngressPath
                {
                    Backend = new k8s.Models.V1IngressBackend
                    {
                        Service = new k8s.Models.V1IngressServiceBackend
                        {
                            Name = serviceName,
                            Port = new k8s.Models.V1ServiceBackendPort
                            {
                                Number = 80
                            }
                        }
                    },
                    Path = $"/{ruleName}/?(.*)",
                    PathType = "ImplementationSpecific"
                });

            }
            V1Patch patch = new V1Patch(ingress, V1Patch.PatchType.MergePatch);
            await client.NetworkingV1.PatchNamespacedIngressAsync(patch, ingressName, k8Namespace);
            return $"{ingress.Spec.Rules[0].Host}/{ruleName}/index.html";
        }

        public async Task RemoveRuleAsync(string k8Namespace, string ingressName, string ruleName)
        {
            var networking = await client.NetworkingV1.ListNamespacedIngressAsync(k8Namespace);
            var ingress = networking.Items.FirstOrDefault(x => x.Metadata.Name.ToUpperInvariant() == ingressName.ToUpperInvariant());
            V1HTTPIngressPath ruleToRemove = default;
            foreach (var rule in ingress.Spec.Rules)
            {
                foreach (var path in rule.Http.Paths.Where(path => path.Path.StartsWith($"/{ruleName}")))
                {
                    ruleToRemove = path;
                }

                rule.Http.Paths.Remove(ruleToRemove);
            }

            V1Patch patch = new V1Patch(ingress, V1Patch.PatchType.MergePatch);
            await client.NetworkingV1.PatchNamespacedIngressAsync(patch, ingressName, k8Namespace);
        }
    }
}
