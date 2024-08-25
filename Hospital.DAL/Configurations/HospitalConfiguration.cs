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
    internal class HospitalConfiguration : IEntityTypeConfiguration<HospitalEntity>
    {
        public void Configure(EntityTypeBuilder<HospitalEntity> builder)
        {
            builder.Property(H => H.Hospital_Name).HasColumnType("VARCHAR(50)");
            builder.Property(H => H.Hospital_Address).HasColumnType("VARCHAR(50)");
            builder.Property(H => H.Hospital_Phone_Number).HasColumnType("VARCHAR(15)");
            builder.Property(H => H.State).HasColumnType("CHAR(2)");
            builder.Property(H => H.Zip_Code).HasColumnType("CHAR(5)");
        }
    }
}
