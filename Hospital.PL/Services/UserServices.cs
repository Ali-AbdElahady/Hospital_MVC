using AutoMapper;
using Hospital.DAL.Context;
using Hospital.DAL.Entites;
using Hospital.PL.Areas.Admin.Models;
using Hospital.PL.Utlities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace Hospital.PL.Services
{
    public class UserServices : IUserServices
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly HospitalDbContext _dbContext;
        private readonly IMapper mapper;

        public UserServices(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            HospitalDbContext dbContext,
            IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<ApplicationUser>> getAllUsersByRole(string roleName, int? departmentId = null, string? searchTerms = null)
        {

            var roles = await _roleManager.Roles.ToListAsync();
            var usersWithIncludes = new List<ApplicationUser>();
            if (!string.IsNullOrEmpty(roleName))
            {
                if (!await _roleManager.RoleExistsAsync(roleName))
                {
                    return new List<ApplicationUser>();
                }

                // Get users in the role using UserManager
                var usersInRole = (await _userManager.GetUsersInRoleAsync(roleName)).ToList();

                // Load related entities like Room, Pharmacy, Department, and Specialization using DbContext
                usersWithIncludes = await _dbContext.Users
                    .Where(u => usersInRole.Select(x => x.Id).Contains(u.Id)) // Filter by the users already retrieved
                    .Include(u => u.Room_Patient)          // Including Room
                    .Include(u => u.Pharmacy)              // Including Pharmacy
                    .Include(u => u.Department)            // Including Department
                    .Include(u => u.Specialization)        // Including Specialization
                    .ToListAsync();

                // Apply filtering by Department_Id if provided
                if (departmentId.HasValue)
                {
                    usersInRole = usersWithIncludes.Where(u => u.Department_ID == departmentId.Value).ToList();
                }

                if (!string.IsNullOrEmpty(searchTerms))
                {
                    usersInRole = usersWithIncludes.Where(u =>
                    (u.FName != null && u.FName.ToLower().Contains(searchTerms.ToLower())) ||
                    (u.LName != null && u.LName.ToLower().Contains(searchTerms.ToLower()))).ToList();
                }
            }
            return usersWithIncludes;
        }
        public async Task<IdentityResult> CreateUser(ApplicationUserVM user)
        {
            var result = new IdentityResult();
            var mappedUser = mapper.Map<ApplicationUserVM, ApplicationUser>(user);
            mappedUser.UserName = UsernameGenerator.GenerateUniqueUsername(mappedUser.FName, mappedUser.LName);
            result = await _userManager.CreateAsync(mappedUser, user.Role == "Doctor" ? "Pa$$w0rdDoctor" : "Pa$$w0rdStaff");
            var userDataData = _dbContext.ApplicationUsers
                                .FirstOrDefault(x => x.UserName == mappedUser.UserName);
            await _userManager.AddToRoleAsync(userDataData, user.Role);
            return result;
        }
        public async Task<ApplicationUser> getUserById(string id)
        {
            return await _dbContext.Users.Where(U => U.Id == id)
                .Include(U => U.Department)
                .Include(U => U.Specialization)
                .Include(U => U.Pharmacy)
                .FirstOrDefaultAsync(); ;
        }
        public async Task<IdentityResult> updateUser(ApplicationUserVM user)
        {
            var result = new IdentityResult();
            var mappedUser = mapper.Map<ApplicationUserVM, ApplicationUser>(user);
            result = await _userManager.UpdateAsync(mappedUser);
            var currentRoles = await _userManager.GetRolesAsync(mappedUser);
            if (!currentRoles.Contains(user.Role))
            {
                result = await _userManager.RemoveFromRolesAsync(mappedUser, currentRoles);
                result = await _userManager.AddToRoleAsync(mappedUser, user.Role);
            }
            return result;
        }
    }
}
