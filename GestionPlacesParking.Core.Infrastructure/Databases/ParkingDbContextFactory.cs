﻿using GestionPlacesParking.Core.Global.EnvironmentVariables.Envs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace GestionPlacesParking.Core.Infrastructure.Databases
{
    public class ParkingDbContextFactory : IDesignTimeDbContextFactory<ParkingDbContext>
    {
        public ParkingDbContext CreateDbContext(string[] args)
        {
            string connectionString = ConnectionStringEnv.ConnectionString;

            var optionsBuilder = new DbContextOptionsBuilder<ParkingDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new ParkingDbContext(optionsBuilder.Options);
        }
    }
}
