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
    public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(item => item.Id);
            builder.ToTable("User");
            builder.Property(item => item.Email).IsRequired(true).HasMaxLength(50);
            builder.Property(item => item.Password).IsRequired(true).HasMaxLength(64);
        }
    }
}
