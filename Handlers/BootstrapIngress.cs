﻿using k8s;
using Microsoft.Extensions.DependencyInjection;

namespace Handlers
{
    public static class BootstrapIngress
    {
        public static void ConnectToKubernetes(this IServiceCollection services)
        {
            var config = KubernetesClientConfiguration.InClusterConfig();

            services.AddSingleton(config);

        }
        public static void InitializeIngress(this IServiceCollection services)
        {
            services.AddSingleton<IIngress, Ingress>();
        }

        public static void InitializeDeployment(this IServiceCollection services)
        {
            services.AddSingleton<IDeployment, Deployment>();
        }

        public static void InitializeService(this IServiceCollection services)
        {
            services.AddSingleton<IService, Service>();
        }
    }
}
