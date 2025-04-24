using Microsoft.EntityFrameworkCore;
using Quantum.ServiceDiscovery.SqlServer;
using System.Data.Common;
using Microsoft.Data.Sqlite;

namespace Quantum.ServiceDiscovery.Tests;

public class ServiceDiscoveryTests
{
    [Fact]
    public async Task Register()
    {
        var options =
            new DbContextOptionsBuilder<ServiceDiscoveryDbContext>()
                .UseSqlite(CreateInMemoryDatabase())
                .Options;
        var serviceDiscoveryDbContext = new ServiceDiscoveryDbContext(options);

        await serviceDiscoveryDbContext.Database.EnsureCreatedAsync();

        IServiceDiscovery serviceDiscovery 
            = new SqlServerServiceDiscovery(serviceDiscoveryDbContext);

        await 
            serviceDiscovery
                .Register(new ServiceRegistration
                {
                    Name = "svc",
                    Port = 2023,
                    Address = "http://localhost",
                    Check = new ServiceCheck
                    {
                        TTL = TimeSpan.MaxValue
                    }
                });

        var resolve = await serviceDiscovery.Resolve("svc");

        Assert.Equal("svc", resolve.Name);
        Assert.Equal("http://localhost", resolve.Address);
        Assert.Equal(2023, resolve.Port);
    }

    private static DbConnection CreateInMemoryDatabase()
    {
        var connection = new SqliteConnection("Filename=:memory:");

        connection.Open();

        return connection;
    }
}