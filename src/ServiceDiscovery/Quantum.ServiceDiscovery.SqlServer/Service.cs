namespace Quantum.ServiceDiscovery.SqlServer;

public class Service
{
    public long  Id { get; set; }
    public string Name { get; set; }
    public string[]? Tags { get; set; } = new string[] { };
    public int Port { get; set; }
    public string Address { get; set; }
    public ServiceCheck Check { get; set; }
    public ServiceStatus Status { get; set; }
    public string ServiceId { get; set; }
}