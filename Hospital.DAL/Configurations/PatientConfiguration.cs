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
    internal class PatientConfiguration : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder.HasOne(p=>p.Pharmacy).WithMany(p=>p.Patients).HasForeignKey(p=>p.Pharmacy_ID).IsRequired(false);
            builder.HasOne(p => p.Room).WithOne(r => r.Patient).HasForeignKey<Room>(p=>p.Patient_ID).IsRequired(false);
        }
    }
}
