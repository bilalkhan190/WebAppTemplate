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
    internal class RoleEntityConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Roles");
            builder.HasKey(x => x.RoleId);
            builder.Property(x => x.RoleName)
                .IsRequired()
                .HasMaxLength(150);
            builder.HasIndex(x => x.RoleName)
                                .IsUnique();
            builder.Property(x => x.RoleId)
       .HasDefaultValueSql("NEWID()");

            builder.HasMany<UserRoles>()
                .WithOne(x => x.Roles)
                .HasForeignKey(x => x.RoleId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.RolePermissions)
                .WithOne(x => x.Role)
                .HasForeignKey(x => x.RoleId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
