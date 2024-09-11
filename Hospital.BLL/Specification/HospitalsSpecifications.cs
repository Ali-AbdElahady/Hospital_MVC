using DemoMvcAgain.BLL.Specifications;
using Hospital.DAL.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.BLL.Specification
{
    public class HospitalsSpecifications : BaseSpecification<HospitalEntity>
    {
        public HospitalsSpecifications(EntitySpecParams Params) : base(H =>
            (string.IsNullOrEmpty(Params.Search) || H.Hospital_Name.ToLower().Contains(Params.Search) || H.Hospital_Address.ToLower().Contains(Params.Search)))
        {
            if (!string.IsNullOrEmpty(Params.Sort))
            {
                switch (Params.Sort)
                {
                    case "NameAsc":
                        AddOrderBy(H => H.Hospital_Name);
                        break;
                    case "NameDesc":
                        AddOrderByDesc(H => H.Hospital_Name);
                        break;
                    default:
                        AddOrderBy(H => H.Id);
                        break;
                }

            }
                ApplyPagination((Params.pageNumber - 1) * Params.PageSize, Params.PageSize);
        }
    }
}
