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
        public int Hospital_ID { get; set; } // Foreign Key
        public HospitalEntity Hospital { get; set; } // Navigation Property
        public ICollection<ApplicationUser> Employees { get; set; } = new HashSet<ApplicationUser>(); // One-to-Many Relationship
    }
}
