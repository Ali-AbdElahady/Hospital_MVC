using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.DAL.Entites
{
    public class Room:BaseEntity
    {
        [Required]
        public int RoomNumber { get; set; }
        public DateTime Admission_Date { get; set; }
        public int? Patient_ID { get; set; } // Optional
        public Patient Patient { get; set; }
        public int? Staff_ID { get; set; }
        public Staff Staff { get; set; } // Navigation Property
    }
}
