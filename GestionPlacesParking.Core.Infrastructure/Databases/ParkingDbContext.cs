using GestionPlacesParking.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionPlacesParking.Core.Infrastructure.Databases
{
    public class ParkingDbContext : DbContext
    {
        public ParkingDbContext(DbContextOptions<ParkingDbContext> options) : base(options)
        {
        }
        public ParkingDbContext()
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new EntityConfigurations.ParkingSlotEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new EntityConfigurations.ReservationEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new EntityConfigurations.UserEntityTypeConfiguration());
        }

        public DbSet<ParkingSlot> ParkingSlots { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
