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
            builder.HasKey(a => new { a.Doctor_ID, a.Patient_ID });
            builder.HasOne(a => a.Doctor).WithMany(D => D.Appointments); 
            builder.HasOne(a => a.Patient).WithMany(P => P.Appointments);
            builder.Property(a=>a.Date).HasColumnType("date");
        }
    }
}
