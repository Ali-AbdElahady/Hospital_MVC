using DemoMvcAgain.BLL.Specifications;
using Hospital.DAL.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.BLL.Specification
{
    public class DepartmentsWithFilterationForCountAsync : BaseSpecification<Department>
    {
        public DepartmentsWithFilterationForCountAsync(DepartmentSpecParams Params) : base(D =>
            (string.IsNullOrEmpty(Params.Search) || D.Department_Name.ToLower().Contains(Params.Search) 
        &&
            (!Params.Hospital_Id.HasValue || D.Hospital_ID == Params.Hospital_Id)))
        {

        }
    }
}
