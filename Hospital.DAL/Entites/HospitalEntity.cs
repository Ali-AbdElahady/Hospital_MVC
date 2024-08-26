using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.DAL.Entites
{
    [Table("Hospitals")]
    public class HospitalEntity : BaseEntity
    {
        public string Hospital_Name { get; set; }
        public string? Hospital_Address { get; set; }
        public string? Hospital_Phone_Number { get; set; }
        public string? State { get; set; }
        public string? Zip_Code { get; set; }
        public ICollection<Department> Departments { get; set; } = new HashSet<Department>(); // One-to-Many Relationship
    }
}
