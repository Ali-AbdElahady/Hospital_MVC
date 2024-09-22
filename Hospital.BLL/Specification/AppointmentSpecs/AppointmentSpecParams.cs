using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.BLL.Specification.AppointmentSpecs
{
    public class AppointmentSpecParams : EntitySpecParams
    {
        public int? departmentId { get; set; }
        public string? doctorId { get; set; }
        public string? PatientId { get; set; }
    }
}
