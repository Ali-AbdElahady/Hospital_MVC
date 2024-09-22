using DemoMvcAgain.BLL.Specifications;
using Hospital.DAL.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.BLL.Specification.AppointmentSpecs
{
    public class AppointmentSpecification : BaseSpecification<Appointment>
    {
        public AppointmentSpecification(AppointmentSpecParams Params):base(A=> 
        (!String.IsNullOrWhiteSpace(Params.doctorId) || A.Doctor_ID == Params.doctorId) &&
        (!String.IsNullOrWhiteSpace(Params.PatientId) || A.Patient_ID == Params.PatientId)
        )
        {
            AddIncludes(D => D.Patient);
            AddIncludes(D => D.Doctor);

            if (!string.IsNullOrEmpty(Params.Sort))
            {
                switch (Params.Sort)
                {
                    case "Doctor":
                        AddOrderBy(A => A.Doctor.FName);
                        break;
                    case "Patient":
                        AddOrderByDesc(A => A.Patient.FName);
                        break;
                    default:
                        AddOrderBy(A => A.Date);
                        break;
                }

            }

            ApplyPagination((Params.pageNumber - 1) * Params.PageSize, Params.PageSize);
        }
        public AppointmentSpecification(string DoctorId,string PatientId) : base(A =>A.Doctor_ID == DoctorId  && A.Patient_ID == PatientId)
        {
            AddIncludes(D => D.Patient);
            AddIncludes(D => D.Doctor);
        }
    }
}
