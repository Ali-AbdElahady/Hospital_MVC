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
        private readonly UserManager<ApplicationUser> userManager;

        public EmployeesController(IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            this.mapper = mapper;
            this.userManager = userManager;
        }
        [Route("")]
        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            var users = await userManager.Users.ToListAsync();
            var mappedEmps = mapper.Map<IEnumerable<ApplicationUser>, IEnumerable<ApplicationUserVM>>(users);
            return View(mappedEmps);
        }
    }
}
