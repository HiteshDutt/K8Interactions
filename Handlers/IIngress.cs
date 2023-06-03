namespace Handlers
{
    public interface IIngress
    {
        string GetIngressAsync(string k8Namespace, string ingressName);
        Task<string> AddRuleAsync(string k8Namespace, string ingressName, string ruleName, string serviceName);
        Task RemoveRuleAsync(string k8Namespace, string ingressName, string ruleName);
    }
}
