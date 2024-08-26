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
        public int Doctor_ID { get; set; }
        public Doctor Doctor { get; set; }
        public int Patient_ID { get; set; }
        public Patient Patient { get; set; }
        public DateTime Date { get; set; }
    }
}
