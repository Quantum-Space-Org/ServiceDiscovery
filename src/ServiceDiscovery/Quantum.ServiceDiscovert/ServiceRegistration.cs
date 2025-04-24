namespace Quantum.ServiceDiscovery;

public class ServiceRegistration
{
    public string Id { get; set; }

    public string Name { get; set; }
    public string[]? Tags { get; set; }
    public int Port { get; set; }
    public string Address { get; set; }
    public ServiceCheck Check { get; set; }
}