using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.DAL.Entites
{
    public class Patient:BaseEntity
    {
        public string Patient_FName { get; set; }
        public string Patient_LName { get; set; }
        public string? Patient_Address { get; set; }
        public string? Patient_Phone_Number { get; set; }
        public int? Pharmacy_ID { get; set; } // Optional
        public Pharmacy? Pharmacy { get; set; } // Navigation Property
        public Room Room { get; set; } // Navigation Property
        public ICollection<Prescription> Prescription { get; set; } = new HashSet<Prescription>();  // One-to-Many Relationship
        public ICollection<Appointment> Appointments { get; set; } = new HashSet<Appointment>();  // One-to-Many Relationship
        public ICollection<Invoice> Invoices { get; set; } = new HashSet<Invoice>();  // One-to-Many Relationship

    }
}
