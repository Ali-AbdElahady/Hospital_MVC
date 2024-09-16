using DemoMvcAgain.BLL.Specifications;
using Hospital.DAL.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.BLL.Specification
{
    public class DepartmentsSpecifications : BaseSpecification<Department>
    {
        public DepartmentsSpecifications(DepartmentSpecParams Params) : base(D =>
            (string.IsNullOrEmpty(Params.Search) || D.Department_Name.ToLower().Contains(Params.Search))
        &&
            (!Params.Hospital_Id.HasValue || D.Hospital_ID == Params.Hospital_Id))
        {
            //Includes.Add(D => D.Hospital);
            AddIncludes(D=>D.Hospital);
            if (!string.IsNullOrEmpty(Params.Sort))
            {
                switch (Params.Sort)
                {
                    case "NameAsc":
                        AddOrderBy(D => D.Department_Name);
                        break;
                    case "NameDesc":
                        AddOrderByDesc(D => D.Department_Name);
                        break;
                    default:
                        AddOrderBy(D => D.Id);
                        break;
                }

            }
            ApplyPagination((Params.pageNumber - 1) * Params.PageSize, Params.PageSize);
        }
        public DepartmentsSpecifications(int id) : base(D => D.Id == id)
        {
            Includes.Add(D => D.Hospital);
        }
    }
}
