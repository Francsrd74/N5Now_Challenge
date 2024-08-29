using AccessControl.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace AccessControl.Infrastructure.Persistence.EFConfigurations
{
    public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {

            builder.Property(e => e.EmployeeForename)
                    .HasMaxLength(300)
                    .IsUnicode(false);

            builder.Property(e => e.EmployeeSurname)
                    .HasMaxLength(300)
                    .IsUnicode(false);

            builder.Property(e => e.PermissionDate).HasColumnType("date");

            builder.HasOne(d => d.PermissionType)
                    .WithMany(p => p.Permissions)
                    .HasForeignKey(d => d.PermissionTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Permissions_PermissionTypes");

        }
    }
}
