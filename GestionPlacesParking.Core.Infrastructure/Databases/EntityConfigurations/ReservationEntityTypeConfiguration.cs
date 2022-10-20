using GestionPlacesParking.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionPlacesParking.Core.Infrastructure.Databases.EntityConfigurations
{
    public class ReservationEntityTypeConfiguration : IEntityTypeConfiguration<Reservation>
    {
        public void Configure(EntityTypeBuilder<Reservation> builder)
        {
            builder.HasKey(item => item.Id);
            builder.ToTable("Reservation");
            builder.Property(item => item.ReservationDate).HasColumnType("Date");
            builder.Property(item => item.ReservingName).IsRequired(true);
            builder.Property(item => item.ReservingName).HasMaxLength(50);
        }
    }
}
