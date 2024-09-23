using Hospital.DAL.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.DAL.Configurations
{
    internal class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> builder)
        {
            builder.HasKey(a => new { a.DoctorID, a.PatientId }).IsClustered(false);
            builder.HasOne(a => a.Doctor).WithMany(D => D.Appointments_Doctor).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(a => a.Patient).WithMany(P => P.Appointments_Patient).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(a => a.Department).WithMany(P => P.Appointments).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(a => a.Hospital).WithMany(P => P.Appointments).OnDelete(DeleteBehavior.Restrict);
            builder.Property(a => a.Date).HasColumnType("date");

            builder.Property(a => a.HospitalId).HasColumnName("Hospital_ID"); 
            builder.Property(a => a.DepartmentId).HasColumnName("Department_ID");

        }
    }
}
