using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Drones.Infrastructure.Persistences.Contexts;

public class DronesContextFactory : IDesignTimeDbContextFactory<DronesContext>
{
    public DronesContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<DronesContext>();
        optionsBuilder.UseSqlServer(
            "Data Source=(localdb)\\MSSQLLocalDB;Database=DronesDB;Trusted_Connection=True;TrustServerCertificate=True",
            b => b.MigrationsAssembly(typeof(DronesContext).Assembly.FullName));

        return new DronesContext(optionsBuilder.Options);
    }
}
