using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.DAL.Entites
{
    public class Doctor : BaseEntity
    {
        public string Doctor_FName { get; set; }
        public string Doctor_LName { get; set; }
        public string? Doctor_Phone_Number { get; set; }
        public string? Doctor_ِAddress { get; set; }
        public int Department_ID { get; set; } // Foreign Key
        public Department Department { get; set; } // Navigation Property
        public ICollection<Appointment> Appointments { get; set; } = new HashSet<Appointment>();  // Many-to-Many Relationshi
    }
}
