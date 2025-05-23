﻿using Hospital.DAL.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.DAL.Configurations
{
    internal class RoomConfiguration : IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> builder)
        {
            builder.HasOne(r => r.Staff).WithMany(s => s.Rooms_Staff).HasForeignKey(r=>r.Staff_ID).IsRequired(false);
            builder.HasOne(r => r.Patient).WithOne(p => p.Room_Patient).HasForeignKey<Room>(r => r.Patient_ID).IsRequired(false);
            builder.Property(r => r.Admission_Date).HasColumnType("date").IsRequired(false);
        }
    }
}
