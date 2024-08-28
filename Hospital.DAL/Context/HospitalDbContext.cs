using Hospital.DAL.Entites;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.DAL.Context
{
    public class HospitalDbContext : IdentityDbContext<ApplicationUser>
    {
        public HospitalDbContext(DbContextOptions<HospitalDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<HospitalEntity> Hospitals { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Pharmacy> Pharmacys { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Medicine> Medicines { get; set; }
        public DbSet<Specialization> Specializations { get; set; }
    }
}
