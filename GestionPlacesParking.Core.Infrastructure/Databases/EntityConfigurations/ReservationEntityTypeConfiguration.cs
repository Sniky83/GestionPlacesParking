using GestionPlacesParking.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GestionPlacesParking.Core.Infrastructure.Databases.EntityConfigurations
{
    public class ReservationEntityTypeConfiguration : IEntityTypeConfiguration<Reservation>
    {
        public void Configure(EntityTypeBuilder<Reservation> builder)
        {
            builder.HasKey(item => item.Id);
            builder.ToTable("Reservation");
            builder.Property(item => item.ReservationDate).HasColumnType("Date");
            builder.Property(item => item.ProprietaireId).HasMaxLength(36);
        }
    }
}
