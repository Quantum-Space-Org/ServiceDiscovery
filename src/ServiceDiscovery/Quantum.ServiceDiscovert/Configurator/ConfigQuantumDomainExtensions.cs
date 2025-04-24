
using System;
using Microsoft.Extensions.DependencyInjection;
using Quantum.Configurator;

namespace Quantum.ServiceDiscovery.Configurator;

public static class ServiceDiscoveryConfiguratorExtensions
{
    public static ConfigServiceDiscoveryBuilder ConfigServiceDiscovery(this QuantumServiceCollection collection)
    {
        return new ConfigServiceDiscoveryBuilder(collection);
    }
}

public class ConfigServiceDiscoveryBuilder(QuantumServiceCollection collection)
{
    public readonly QuantumServiceCollection _collection = collection;

    public ConfigServiceDiscoveryBuilder RegisterServiceDiscovery<T>() where T : IServiceDiscovery
    {
        _collection.Collection.AddSingleton(typeof(IServiceDiscovery), typeof(T));

        return this;
    }

    public ConfigServiceDiscoveryBuilder RegisterServiceDiscovery(Func<IServiceProvider, IServiceDiscovery> function)
    {
        _collection.Collection.AddSingleton(typeof(IServiceDiscovery), function);

        return this;
    }

    public QuantumServiceCollection and()
    {
        return _collection;
    }
}