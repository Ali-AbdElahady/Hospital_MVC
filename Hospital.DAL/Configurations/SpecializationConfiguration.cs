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
    internal class SpecializationConfiguration : IEntityTypeConfiguration<Specialization>
    {
        public void Configure(EntityTypeBuilder<Specialization> builder)
        {
            builder.HasMany(s=>s.Doctors).WithOne(a=>a.Specialization).HasForeignKey(a=>a.Specialization_ID).IsRequired(false);
        }
    }
}
