using GestionPlacesParking.Core.Models;
using Microsoft.EntityFrameworkCore;

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
            modelBuilder.ApplyConfiguration(new EntityConfigurations.AdminEntityTypeConfiguration());
        }

        public DbSet<ParkingSlot> ParkingSlots { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
