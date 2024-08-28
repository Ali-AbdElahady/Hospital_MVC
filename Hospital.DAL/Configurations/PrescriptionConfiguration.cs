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
    internal class PrescriptionConfiguration : IEntityTypeConfiguration<Prescription>
    {
        public void Configure(EntityTypeBuilder<Prescription> builder)
        {
            builder.HasOne(p => p.Patient).WithMany(p => p.Prescriptions).HasForeignKey(p=>p.Patient_ID);
            builder.Property(p => p.Prescription_Date).HasColumnType("date");
        }
    }
}
