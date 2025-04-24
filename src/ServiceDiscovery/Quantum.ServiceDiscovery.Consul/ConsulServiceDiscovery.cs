using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Consul;

namespace Quantum.ServiceDiscovery.Consul;

public class ConsulServiceDiscovery(ConsulClientConfiguration config) : IServiceDiscovery
{
    public async Task Register(ServiceRegistration serviceRegistration)
    {
        using var consulClient = new ConsulClient(config);
        await consulClient.Agent.ServiceRegister(new AgentServiceRegistration
        {
            Name = serviceRegistration.Name,
            ID = Guid.NewGuid().ToString(),
            Address = serviceRegistration.Address,
            Port = serviceRegistration.Port,
            Tags = serviceRegistration.Tags,
            Check = new AgentCheckRegistration
            {
                TTL = serviceRegistration.Check.TTL
            }
        });
    }

    public async Task<ServiceRegistrationViewModel> Resolve(string serviceName)
    {
        using var consulClient = new ConsulClient(config);
            
        //Get all services registered on Consul
        var allRegisteredServices = await consulClient.Agent.Services();

        //Get all instance of the service went to send a request to
        var registeredServices = allRegisteredServices.Response?.Where(s => s.Value.Service.Equals(serviceName, StringComparison.OrdinalIgnoreCase)).Select(x => x.Value).ToList();

        //Get a random instance of the service
        var service = GetRandomInstance(registeredServices);

        if (service == null)
            throw new ConsulServiceNotFoundException($"Consul service: '{serviceName}' was not found.", serviceName);

        return new ServiceRegistrationViewModel
        {
            Address = service.Address,
            Port= service.Port,
            Tags = service.Tags
        };
    }

    private static AgentService GetRandomInstance(IList<AgentService> services) 
        => services[new Random().Next(0, services.Count)];
}