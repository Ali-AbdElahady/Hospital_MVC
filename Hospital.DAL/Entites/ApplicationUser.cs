using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.DAL.Entites
{
    public class ApplicationUser : IdentityUser
    {
        public string FName { get; set; }
        public string LName { get; set; }
        public string? Address { get; set; }
        public int Specialization_ID { get; set; } //  Foreign Key of Specialization for Doctor
        public Specialization Specialization { get; set; } // Navigational Property
        public int Department_ID { get; set; } // Foreign Key of Department for Staff And Doctor
        public Department Department { get; set; } // Navigational Property
        public int Pharmacy_ID  { get; set; } // Foreign Key of Pharmacy for patient
        public Pharmacy Pharmacy { get; set; } // Navigational Property
        public ICollection<Appointment> Appointments_Doctor { get; set; } = new List<Appointment>(); // One-to-Many Relationship
        public ICollection<Appointment> Appointments_Patient { get; set; } = new HashSet<Appointment>(); // One-to-Many Relationship
        public ICollection<Room> Rooms_Staff { get; set; } = new HashSet<Room>(); // One-to-Many Relationship
        public Room Room_Patient { get; set; }
        public ICollection<Prescription> Prescriptions { get; set; } = new HashSet<Prescription>();  // One-to-Many Relationship
        public ICollection<Invoice> Invoices { get; set; } = new HashSet<Invoice>();  // One-to-Many Relationship

    }
}
