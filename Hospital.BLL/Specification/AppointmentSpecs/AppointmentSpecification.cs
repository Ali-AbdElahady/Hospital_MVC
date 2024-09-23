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
        (!String.IsNullOrWhiteSpace(Params.doctorId) || A.DoctorID == Params.doctorId) &&
        (!String.IsNullOrWhiteSpace(Params.PatientId) || A.PatientId == Params.PatientId)
        )
        {
            AddIncludes(D => D.Patient);
            AddIncludes(D => D.Doctor);
            AddIncludes(D => D.Hospital);
            AddIncludes(D => D.Department);

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
        public AppointmentSpecification(string DoctorId,string PatientId) : base(A =>A.DoctorID == DoctorId  && A.PatientId == PatientId)
        {
            AddIncludes(D => D.Patient);
            AddIncludes(D => D.Doctor);
            AddIncludes(D => D.Hospital);
            AddIncludes(D => D.Department);
        }
    }
}
