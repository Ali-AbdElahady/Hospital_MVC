using AutoMapper;
using Hospital.DAL.Entites;
using Hospital.PL.Areas.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hospital.PL.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Employees")]
    [Authorize(Roles = "Admin")]
    public class EmployeesController : Controller
    {
        private readonly IMapper mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationUser> _roleManager;

        public EmployeesController(IMapper mapper, UserManager<ApplicationUser> userManager, RoleManager<ApplicationUser> roleManager)
        {
            this.mapper = mapper;
            this._userManager = userManager;
            this._roleManager = roleManager;
        }
        [Route("")]
        [Route("Index")]
        public async Task<IActionResult> Index(string roleName)
        {
            if (!await _roleManager.RoleExistsAsync(roleName))
            {
                return NotFound($"Role '{roleName}' does not exist.");
            }

            // Fetch users in the role
            var usersInRole = await _userManager.GetUsersInRoleAsync(roleName);

            var users = await _userManager.Users.ToListAsync();
            var mappedEmps = mapper.Map<IEnumerable<ApplicationUser>, IEnumerable<ApplicationUserVM>>(users);
            return View(mappedEmps);
        }
    }
}
