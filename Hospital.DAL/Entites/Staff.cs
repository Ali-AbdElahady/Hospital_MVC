using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.DAL.Entites
{
    public class Staff : BaseEntity
    {
        public string Staff_FName { get; set; }
        public string Staff_LName { get; set; }
        public string? Staff_Address { get; set; }
        public string? Staff_Phone_Number { get; set; }
        public int Department_ID { get; set; }
        public Department Department { get; set; }
        public ICollection<Room> AssignedRooms { get; set; }  // One-to-Many Relationship
    }
}
