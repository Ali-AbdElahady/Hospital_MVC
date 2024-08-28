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
        public string Doctor_ID { get; set; }
        public ApplicationUser Doctor { get; set; }
        public string Patient_ID { get; set; }
        public ApplicationUser Patient { get; set; }
        public DateTime Date { get; set; }
    }
}
