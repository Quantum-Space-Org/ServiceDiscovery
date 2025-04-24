using Consul;
using Microsoft.Extensions.DependencyInjection;
using Quantum.ServiceDiscovery.Configurator;

namespace Quantum.ServiceDiscovery.Consul.Configurator;

public static class ServiceDiscoveryConfiguratorExtensions
{
    public static ConsulServiceDiscoveryBuilder  RegisterConsulServiceDiscovery(this ConfigServiceDiscoveryBuilder serviceDiscoveryBuilder)
    {
        return new ConsulServiceDiscoveryBuilder(serviceDiscoveryBuilder);
    }
}

public class ConsulServiceDiscoveryBuilder(ConfigServiceDiscoveryBuilder serviceDiscoveryBuilder)
{
    public ConfigServiceDiscoveryBuilder ServiceDiscoveryBuilder { get; } = serviceDiscoveryBuilder;

    public ConsulServiceDiscoveryBuilder RegisterConsulClient(ConsulClientConfiguration config)
    {
        ServiceDiscoveryBuilder
            .RegisterServiceDiscovery<ConsulServiceDiscovery>();


        ServiceDiscoveryBuilder
            ._collection.Collection.AddSingleton<ConsulClientConfiguration>(config);

        return this;
    }
}