using System;
using Consul;
using Microsoft.Extensions.Configuration;
using Winton.Extensions.Configuration.Consul;

namespace Quantum.ConfigurationManagement;

public static class ConfigurationBuilderExtension
{
    public static ConfigurationBuilder UseConsul(this QuantumConfigurationBuilder builder
        , string environment, string application, Action<ConsulClientConfiguration>? configuration = null)
        => Add(builder,$"{application}/{environment}", configuration);

    public static ConfigurationBuilder UseConsul(this QuantumConfigurationBuilder builder,
        string application, Action<ConsulClientConfiguration>? configuration = null)
        => Add(builder, application, configuration);
    
    public static ConfigurationBuilder UseConsul(this QuantumConfigurationBuilder builder,
        Action<ConsulClientConfiguration>? configuration = null) => Add(builder,"Shared", configuration);
    
    private static ConfigurationBuilder Add(this QuantumConfigurationBuilder builder,
        string application, Action<ConsulClientConfiguration>? configuration = null)
    {
        if (configuration is not null)
            builder.WithConfigurationRoot().AddConsul(application, options => options.ConsulConfigurationOptions = configuration);
        
        else
            builder.WithConfigurationRoot().AddConsul(application);

        return builder;
    }
}