namespace BlazorApp1.Services
{
    public class ServiceBase
    {
        public string CreateApiUrl(string serviceName)
        {
            return $"http://localhost:5115/api/{serviceName}";
        }
    }
}
