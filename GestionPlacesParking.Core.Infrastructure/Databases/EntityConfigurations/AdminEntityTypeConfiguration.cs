using GestionPlacesParking.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GestionPlacesParking.Core.Infrastructure.Databases.EntityConfigurations
{
    public class AdminEntityTypeConfiguration : IEntityTypeConfiguration<Admin>
    {
        public void Configure(EntityTypeBuilder<Admin> builder)
        {
            builder.HasKey(item => item.Id);
            builder.ToTable("Admin");
            builder.Property(item => item.Email).IsRequired(true).HasMaxLength(50);
        }
    }
}
