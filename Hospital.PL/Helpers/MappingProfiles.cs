using AutoMapper;
using Hospital.DAL.Entites;
using Hospital.PL.Areas.Admin.Models;

namespace Hospital.PL.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Department,DepartmentVM>()
                .ForMember(D=>D.Department_Name , O=>O.MapFrom(S=>S.Department_Name))
                .ForMember(D => D.Hospital_Id, O => O.MapFrom(S => S.Hospital_ID))
                .ForMember(D => D.Hospital, O => O.MapFrom(S => S.Hospital))
                .ReverseMap();
            CreateMap<ApplicationUser,ApplicationUserVM>().ReverseMap();
        }
    }
}
