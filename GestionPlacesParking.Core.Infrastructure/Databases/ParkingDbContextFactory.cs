using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace GestionPlacesParking.Core.Infrastructure.Databases
{
    public class ParkingDbContextFactory : IDesignTimeDbContextFactory<ParkingDbContext>
    {
        public ParkingDbContext CreateDbContext(string[] args)
        {
            string path = Directory.GetCurrentDirectory();
            string parent = Directory.GetParent(path).ToString();

            IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(parent + "\\GestionPlacesParking\\")
            .AddJsonFile("appsettings.json")
            .Build();

            var connectionString = configuration.GetConnectionString("ParkingContext");

            var optionsBuilder = new DbContextOptionsBuilder<ParkingDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new ParkingDbContext(optionsBuilder.Options);
        }
    }
}
