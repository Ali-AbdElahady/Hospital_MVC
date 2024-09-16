using DemoMvcAgain.BLL.Specifications;
using Hospital.DAL.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.BLL.Specification.HospitalSpecs
{
    public class HospitalsWithFilterationForCountAsync : BaseSpecification<HospitalEntity>
    {
        public HospitalsWithFilterationForCountAsync(EntitySpecParams Params) : base(P =>
            string.IsNullOrEmpty(Params.Search) || P.Hospital_Name.ToLower().Contains(Params.Search) || P.Hospital_Address.ToLower().Contains(Params.Search))
        {

        }
    }
}
