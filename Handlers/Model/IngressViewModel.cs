namespace Handlers
{
    public class IngressViewModel
    {
        public string K8Namespace { get; set; }
        public string ServiceName { get; set; }
        public string RuleName { get; set; }
        public string IngressName { get; set; }
    }
}
