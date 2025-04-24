using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;

namespace Quantum.ServiceDiscovery.Extensions;

public static class ApplicationBuilderExtensions
{
    public static void RegisterMeToServiceDiscovery(this IApplicationBuilder app
        , ServiceRegistration serviceRegistration)
    {
        Register(app, serviceRegistration);
    }

    public static void RegisterMeToServiceDiscovery(this IApplicationBuilder app
        , string name, TimeSpan? timeSpan = null)
    {
        var server = app.ApplicationServices.GetService(typeof(IServer)) as IServer;

        var addressFeature = server.Features.Get<IServerAddressesFeature>();

        foreach (var address in addressFeature.Addresses)
        {
            var serverAddress = new Uri(address);

            Register(app, new ServiceRegistration
            {
                Address = serverAddress.AbsoluteUri,
                Port = serverAddress.Port,
                Name = name,
                Check = new ServiceCheck
                {
                    TTL = timeSpan ?? TimeSpan.FromSeconds(10)
                }
            });
        }
    }

    private static void Register(IApplicationBuilder app, ServiceRegistration serviceRegistration)
    {
        var service = app.ApplicationServices
            .GetService(typeof(IServiceDiscovery));

        (service as IServiceDiscovery)
            .Register(serviceRegistration);
    }
}