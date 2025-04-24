using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Quantum.ServiceDiscovery.SqlServer;

public class SqlServerServiceDiscovery(ServiceDiscoveryDbContext context) : IServiceDiscovery
{
    public async Task Register(ServiceRegistration serviceRegistration)
    {
        context.Services.Add(new Service
        {
            Address = serviceRegistration.Address,
            Name = serviceRegistration.Name,
            ServiceId = serviceRegistration.Id,
            Check = serviceRegistration.Check,
            Port = serviceRegistration.Port,
            Status = ServiceStatus.Alive,
            Tags = serviceRegistration.Tags
        });

        await context.SaveChangesAsync();
    }

    public async Task<ServiceRegistrationViewModel> Resolve(string serviceName)
    {
        var service = await context.Services.FirstOrDefaultAsync(f => f.Name == serviceName);
        return new ServiceRegistrationViewModel
        {
            Name = service.Name,
            Id= service.ServiceId,
            Port = service.Port,
            Address = service.Address,
            Status = service.Status,
            Tags = service.Tags,
            Check = service.Check,
        };
    }
}