using Hospital.DAL.Entites;
using Hospital.PL.Areas.Admin.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Hospital.PL.Services
{
    public class UserServices : IUserServices
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserServices(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this._userManager = userManager;
            this._roleManager = roleManager;
        }

        public async Task<IEnumerable<ApplicationUser>> getAllUsersByRole(string roleName, int? departmentId = null, string? searchTerms = null)
        {

            var roles = await _roleManager.Roles.ToListAsync();
            var usersInRole = new List<ApplicationUser>();
            if (!string.IsNullOrEmpty(roleName))
            {
                if (!await _roleManager.RoleExistsAsync(roleName))
                {
                    return null;
                }

                usersInRole = (List<ApplicationUser>)await _userManager.GetUsersInRoleAsync(roleName);
                // Apply filtering by Department_Id if provided

                if (departmentId.HasValue)
                {
                    usersInRole = usersInRole.Where(u => u.Department_ID == departmentId.Value).ToList();
                }

                if(!string.IsNullOrEmpty(searchTerms))
                {
                    usersInRole = usersInRole.Where(u=>
                    u.FName.ToLower().Contains(searchTerms.ToLower()) || 
                    u.LName.ToLower().Contains(searchTerms.ToLower())).ToList();
                }
            }
            return usersInRole;
        }
    }
}
