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
    public class ParkingSlotEntityTypeConfiguration : IEntityTypeConfiguration<ParkingSlot>
    {
        public void Configure(EntityTypeBuilder<ParkingSlot> builder)
        {
            builder.HasKey(item => item.Id);
            builder.ToTable("ParkingSlot");
            builder.Property(item => item.Label).IsRequired(true);
            builder.Property(item => item.Label).HasMaxLength(50);
        }
    }
}
