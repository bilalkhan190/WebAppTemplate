using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppTemplate.Domain.Entities;

namespace WebAppTemplate.Infrastructure.Persistance.Configurations
{
    public class UserRoleEntityConfiguration : IEntityTypeConfiguration<UserRoles>
    {
        public void Configure(EntityTypeBuilder<UserRoles> builder)
        {
            builder.ToTable("UserRoles");

            // Define composite primary key
            builder.HasKey(ur => new { ur.UserId, ur.RoleId });

            // Configure properties
            builder.Property(ur => ur.UserId)
                .IsRequired();

            builder.Property(ur => ur.RoleId)
                .IsRequired();

            // Configure relationships
            builder.HasOne(ur => ur.Users) 
                .WithMany(u => u.UserRoles) 
                .HasForeignKey(ur => ur.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(ur => ur.Roles) 
                .WithMany(x => x.UserRoles) 
                .HasForeignKey(ur => ur.RoleId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
