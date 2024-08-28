using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.DAL.Entites
{
    public class Prescription : BaseEntity
    {
        public string Patient_ID { get; set; }
        public ApplicationUser Patient { get; set; }
        public ICollection<Medicine> Medicine { get; set; } = new HashSet<Medicine>();
        public DateTime Prescription_Date { get; set; }
        public decimal Prescription_Cost { get; set; }
    }
}
