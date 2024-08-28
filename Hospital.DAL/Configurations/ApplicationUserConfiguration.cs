using Hospital.DAL.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.DAL.Configurations
{
    internal class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.HasOne(p => p.Pharmacy).WithMany(p => p.Patients).HasForeignKey(p => p.Pharmacy_ID).IsRequired(false);
            builder.HasOne(p => p.Room_Patient).WithOne(r => r.Patient).HasForeignKey<Room>(p => p.Patient_ID).IsRequired(false);
            builder.HasOne(d => d.Department).WithMany(d => d.Employees).HasForeignKey(d => d.Department_ID).IsRequired(false);
        }
    }
}
