using System;

namespace Quantum.ServiceDiscovery.Consul;

public class ConsulServiceNotFoundException(string message, string serviceName) : Exception(message)
{
    public string ServiceName { get; } = serviceName;
}