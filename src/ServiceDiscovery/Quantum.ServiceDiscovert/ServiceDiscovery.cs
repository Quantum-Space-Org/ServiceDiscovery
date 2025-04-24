namespace Quantum.ServiceDiscovery;

public class ServiceRegistrationViewModel
{
    public string Id { get; set; }
    public string Name { get; set; }
    public int Port { get; set; }
    public string Address { get; set; }
    public ServiceStatus Status { get; set; }
    public string[]? Tags { get; set; }
    public ServiceCheck Check { get; set; }
}