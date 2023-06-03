//using System.Text.Json.Serialization;

//namespace Handlers
//{
//    internal class IngressModel
//    {
//        [JsonPropertyName("apiVersion")]
//        public string ApiVersion { get { return "networking.k8s.io/v1"; } }

//        [JsonPropertyName("kind")]
//        public string Kind { get { return "Ingress"; } }
//    }

//    internal class MetadataModel
//    {
//        [JsonPropertyName("name")]
//        public string Name { get; set; }

//        [JsonPropertyName("annotations")]
//        public AnnotationClass Annotations { get; set; }
//    }

//    internal class AnnotationClass
//    {
//        [JsonPropertyName("kubernetes.io/ingress.class")]
//        public string K8IngressClass { get { return "nginx"; } }

//        [JsonPropertyName("nginx.ingress.kubernetes.io/configuration-snippet")]
//        public string ConfigurationSnippet { get; set; }

//        ///TODO more line items if needed
//    }

//    internal class SpecModel
//    {
//        ////[JsonPropertyName("ingressClassName")]
//        ////public string IngressClassName { get; set; }

//        [JsonPropertyName("rules")]
//        public List<RulesModel> Rules { get; set; }
//    }

//    internal class RulesModel
//    {
//        [JsonPropertyName("host")]
//        public string Host { get; set; }
//        [JsonPropertyName("http")]
//        public string Http { get; set; }
//    }

//    internal class HttpModel
//    {
//        [JsonPropertyName("paths")]
//        public PathsModel Paths { get; set; }
//    }

//    internal class PathsModel
//    {
//        [JsonPropertyName("backend")]
//        public BackendModel Backend { get; set; }
//        [JsonPropertyName("path")]
//        public string Path { get; set; }
//        [JsonPropertyName("pathType")]
//        public string pathType { get; set; }
//    }

//    internal class BackendModel
//    {
//        [JsonPropertyName("service")]
//        public ServiceModel Service { get; set; }
//    }

//    internal class ServiceModel
//    {
//        [JsonPropertyName("name")]
//        public string Name { get; set; }
//        [JsonPropertyName("port")]
//        public PortModel Port { get; set; }
//    }

//    internal class PortModel
//    {
//        [JsonPropertyName("number")]
//        public int Number { get; set; }
//    }
//}