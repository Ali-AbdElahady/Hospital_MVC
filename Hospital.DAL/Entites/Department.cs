using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.DAL.Entites
{
    public class Department : BaseEntity
    {
        public string Department_Name { get; set; }
        public int Hospital_ID { get; } // Foreign Key
        public HospitalEntity Hospital { get; set; } // Navigation Property
        public ICollection<Doctor> Doctors { get; set; } = new HashSet<Doctor>(); // One-to-Many Relationship
        public ICollection<Staff> Staffs { get; set; } = new HashSet<Staff>();  // One-to-Many Relationship
    }
}
