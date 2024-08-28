using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.DAL.Entites
{
    public class Specialization : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<ApplicationUser> Doctors { get; set; } = new List<ApplicationUser>();
    }
}
