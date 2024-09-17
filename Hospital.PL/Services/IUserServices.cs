using Hospital.DAL.Entites;

namespace Hospital.PL.Services
{
    public interface IUserServices
    {
        Task<IEnumerable<ApplicationUser>> getAllUsersByRole(string roleName, int? departmentId = null, string? searchTerms = null);
    }
}
