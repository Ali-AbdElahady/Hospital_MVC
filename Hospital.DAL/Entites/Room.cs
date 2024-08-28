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
        public DateTime? Admission_Date { get; set; }
        public string? Patient_ID { get; set; } // Optional
        public ApplicationUser Patient { get; set; } // Navigation Property
        public string? Staff_ID { get; set; } // Optional
        public ApplicationUser Staff { get; set; } // Navigation Property
    }
}
