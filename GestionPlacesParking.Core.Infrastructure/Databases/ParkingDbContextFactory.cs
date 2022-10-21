using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionPlacesParking.Core.Infrastructure.Databases
{
    public class ParkingDbContextFactory : IDesignTimeDbContextFactory<ParkingDbContext>
    {
        public ParkingDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ParkingDbContext>();
            optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=GestionPlacesParking;Trusted_Connection=True;");

            return new ParkingDbContext(optionsBuilder.Options);
        }
    }
}
