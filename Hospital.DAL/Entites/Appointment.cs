using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.DAL.Entites
{
    public class Appointment 
    {
        public string DoctorID { get; set; }
        public ApplicationUser Doctor { get; set; }
        public string PatientId { get; set; }
        public ApplicationUser Patient { get; set; }
        public DateTime Date { get; set; }
        public int HospitalId { get; set; }  
        public HospitalEntity Hospital { get; set; }
        public int DepartmentId { get; set; } 
        public Department Department { get; set; }
    }
}
