using Hospital.DAL.Entites;
using Hospital.PL.Areas.Admin.Models;
using Microsoft.AspNetCore.Identity;

namespace Hospital.PL.Services
{
    public interface IUserServices
    {
        Task<IEnumerable<ApplicationUser>> getAllUsersByRole(string roleName, int? departmentId = null, string? searchTerms = null);
        Task<IdentityResult> CreateUser(ApplicationUserVM user);
        Task<ApplicationUser> getUserById(string id);
        Task<IdentityResult> updateUser(ApplicationUserVM user);
    }
}
