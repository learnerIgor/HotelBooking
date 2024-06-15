namespace Auth.ExternalProviders.Exceptions
{
    public class ExternalServiceNotAvailable : Exception
    {
        public ExternalServiceNotAvailable(string serviceName, string message) : base($"{serviceName},{message}") { }
    }
}
