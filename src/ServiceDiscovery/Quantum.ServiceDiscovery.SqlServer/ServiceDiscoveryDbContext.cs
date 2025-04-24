using Microsoft.EntityFrameworkCore;

namespace Quantum.ServiceDiscovery.SqlServer;

public class ServiceDiscoveryDbContext(DbContextOptions<ServiceDiscoveryDbContext> options) : DbContext(options)
{
    public DbSet<Service> Services { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Service>()
            .Property(p => p.Id)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<Service>()
            .Property(p => p.Tags)
            .HasConversion(
                value => Newtonsoft.Json.JsonConvert.SerializeObject(value),
                value =>
                    string.IsNullOrWhiteSpace(value)
                        ? new string[] { }
                        : Newtonsoft.Json.JsonConvert.DeserializeObject<string[]>(value)
            );


        modelBuilder.Entity<Service>()
            .Property(p => p.Check)
            .HasConversion(
                value => Newtonsoft.Json.JsonConvert.SerializeObject(value),
                value =>
                    string.IsNullOrWhiteSpace(value)
                        ? new ServiceCheck()
                        : Newtonsoft.Json.JsonConvert.DeserializeObject<ServiceCheck>(value)
            );

        base.OnModelCreating(modelBuilder);
    }
}