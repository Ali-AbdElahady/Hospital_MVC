using DemoMvcAgain.BLL.Specifications;
using Hospital.DAL.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.BLL.Specification.AppointmentSpecs
{
    public class AppointmnetWithFilterationForCountAsync : BaseSpecification<Appointment>
    {
        public AppointmnetWithFilterationForCountAsync(AppointmentSpecParams Params) :base(A =>
            (String.IsNullOrWhiteSpace(Params.doctorId) || A.DoctorID == Params.doctorId) &&
            (String.IsNullOrWhiteSpace(Params.PatientId) || A.PatientId == Params.PatientId)
        )
        {
            
        }
    }
}
