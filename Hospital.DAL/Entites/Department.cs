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
        [ForeignKey(nameof(Hospital))]
        public string? Hospital_ID { get;}
        public HospitalEntity? Hospital { get; set; }
    }
}
