using System.Threading.Tasks;

namespace Quantum.ServiceDiscovery;

public interface IServiceDiscovery
{
    Task Register(ServiceRegistration serviceRegistration);
    Task<ServiceRegistrationViewModel> Resolve(string serviceName);
}