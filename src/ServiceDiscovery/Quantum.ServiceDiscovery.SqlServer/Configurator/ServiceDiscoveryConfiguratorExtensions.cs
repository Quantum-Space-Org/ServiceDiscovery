using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Quantum.ServiceDiscovery.Configurator;

namespace Quantum.ServiceDiscovery.SqlServer.Configurator;

public static class ServiceDiscoveryConfiguratorExtensions
{
    public static RegisterSqlServerServiceDiscoveryBuilder RegisterSqlServerServiceDiscovery(
        this ConfigServiceDiscoveryBuilder serviceDiscoveryBuilder) => new(serviceDiscoveryBuilder);
}

public class RegisterSqlServerServiceDiscoveryBuilder(ConfigServiceDiscoveryBuilder serviceDiscoveryBuilder)
{
    public ConfigServiceDiscoveryBuilder RegisterDbContextOptions(
        DbContextOptionsBuilder<ServiceDiscoveryDbContext> dbContext)
    {
        serviceDiscoveryBuilder._collection.Collection.AddSingleton(dbContext.Options);
        serviceDiscoveryBuilder.RegisterServiceDiscovery<SqlServerServiceDiscovery>();

        return serviceDiscoveryBuilder;
    }
}