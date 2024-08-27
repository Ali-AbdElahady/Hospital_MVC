using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.DAL.Entites
{
    public class Medicine : BaseEntity
    {
        public int Prescription_ID { get; set; }
        public Prescription Prescription { get; set; }
        public string Medicine_Name { get; set; }
        public string Medicine_Description { get; set; }
    }
}
