using System.Data;
using k8s;
using k8s.Models;

namespace Handlers
{
    internal class Deployment : IDeployment
    {
        private readonly Kubernetes client;

        public Deployment()
        {
            var config = KubernetesClientConfiguration.BuildConfigFromConfigFile();
            client = new Kubernetes(config);
        }

        public Task CreateDeploymentAsync(string k8Namespace, string appName, string threeDImageRelativePath)
        {
            var deployment = new V1Deployment
            {
                ApiVersion = "apps/v1",
                Kind = "Deployment",
                Metadata = new V1ObjectMeta
                {
                    Name = appName
                },
                Spec = new V1DeploymentSpec
                {
                    Replicas = 1,
                    Selector = new V1LabelSelector {
                        MatchLabels = new Dictionary<string, string>(){ { "app", appName } },
                    },
                    Template = new V1PodTemplateSpec
                    {
                        Metadata = new V1ObjectMeta
                        {
                            Labels = new Dictionary<string, string> { { "app", appName } }
                        },
                        Spec = new V1PodSpec
                        {
                            Containers = new List<V1Container>
                            {
                                new V1Container
                                {
                                    Env = new List<V1EnvVar>
                                    {
                                        new V1EnvVar
                                        {
                                            Name = "AZ_BATCH_NODE_MOUNTS_DIR",
                                            Value = "/mnt/blobfuse"
                                        }
                                    },
                                    Name = $"{appName}-pod",
                                    Image = "acr615cd13515c55faf8416ace91951b833.azurecr.io/threedvisual/vizer",
                                    Command = new List<string>
                                    {
                                        "/usr/bin/bash",
                                        "-c"
                                    },
                                    Args = new List<string>
                                    {
                                        $"/opt/paraview/bin/pvpython /opt/vizer/server.py --server --venv /opt/trame/env -i 0.0.0.0 -p 80 --dataset {{AZ_BATCH_NODE_MOUNTS_DIR}}/{threeDImageRelativePath}"
                                    },
                                    Ports = new List<V1ContainerPort> {
                                        new V1ContainerPort
                                        {
                                            ContainerPort= 80,
                                            Protocol = "TCP"
                                        }
                                    },
                                    VolumeMounts = new List<V1VolumeMount>
                                    {
                                        new V1VolumeMount
                                        {
                                            Name = "pvc-blobfuse",
                                            MountPath = "/mnt/blobfuse"
                                        }
                                    },
                                    Resources = new V1ResourceRequirements
                                    {
                                        Requests= new Dictionary<string, ResourceQuantity>
                                        {
                                            {"cpu", new ResourceQuantity{ Value = "10" } },
                                            {"memory", new ResourceQuantity{ Value = "20Gi" } }
                                        },
                                        Limits= new Dictionary<string, ResourceQuantity>
                                        {
                                            {"cpu", new ResourceQuantity{ Value = "20" } },
                                            {"memory", new ResourceQuantity{ Value = "40Gi" } }
                                        }
                                    }
                                }
                            },
                            Volumes = new List<V1Volume> { 
                                new V1Volume
                                {
                                    Name = "pvc-blobfuse",
                                    PersistentVolumeClaim = new V1PersistentVolumeClaimVolumeSource
                                    {
                                        ClaimName = "pvc3-blob"
                                    }
                                }
                            },
                            NodeSelector = new Dictionary<string, string>
                            {
                                { "agentpool","threedpool" }
                            }
                        }
                        
                    }
                }
            };
            return client.AppsV1.CreateNamespacedDeploymentAsync(deployment, k8Namespace);
        }

        public Task RemoveDeploymentAsync(string k8Namespace, string appName)
        {
            return client.DeleteNamespacedDeploymentAsync(appName, k8Namespace);
        }
    }
}
