using Hospital.DAL.Entites;

namespace Hospital.PL.Areas.Admin.Models
{
    public class ApplicationUserVM
    {
        public string Id { get; set; } // Change from int to string
        public string FName { get; set; }
        public string LName { get; set; }
        public string? Address { get; set; }
        public int? Specialization_ID { get; set; } // Foreign Key of Specialization for Doctor
        public Specialization Specialization { get; set; } // Navigational Property
        public int? Department_ID { get; set; } // Foreign Key of Department for Staff And Doctor
        public Department Department { get; set; } // Navigational Property
        public int? Pharmacy_ID { get; set; } // Foreign Key of Pharmacy for patient
        public Pharmacy Pharmacy { get; set; } // Navigational Property
    }
}
