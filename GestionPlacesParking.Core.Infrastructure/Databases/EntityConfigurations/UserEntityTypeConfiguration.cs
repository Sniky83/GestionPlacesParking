using GestionPlacesParking.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GestionPlacesParking.Core.Infrastructure.Databases.EntityConfigurations
{
    public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(item => item.Id);
            builder.ToTable("User");
            builder.Property(item => item.Email).IsRequired(true).HasMaxLength(50);
            builder.Property(item => item.FirstName).IsRequired(true).HasMaxLength(50);
            builder.Property(item => item.LastName).IsRequired(true).HasMaxLength(50);
            builder.Property(item => item.Password).IsRequired(true).HasMaxLength(64);
        }
    }
}
