using AutoMapper;
using Hospital.BLL.Interfaces;
using Hospital.DAL.Entites;
using Hospital.PL.Areas.Admin.Models;
using Hospital.PL.Helpers;
using Hospital.PL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Hospital.PL.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Employees")]
    [Authorize(Roles = "Admin")]
    public class EmployeesController : Controller
    {
        private readonly IMapper mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserServices _userServices;

        public EmployeesController(IMapper mapper, 
            UserManager<ApplicationUser> userManager, 
            RoleManager<IdentityRole> roleManager ,
            IUnitOfWork unitOfWork,
            IUserServices userServices)
        {
            this.mapper = mapper;
            this._userManager = userManager;
            this._roleManager = roleManager;
            this._unitOfWork = unitOfWork;
            this._userServices = userServices;
        }
        [Route("")]
        [Route("Index")]
        public async Task<IActionResult> Index(EmpsParams param)
        {
            var departments = await _unitOfWork.GenerateGenericRepo<Department>().GetAllAsync();
            var roles = await _roleManager.Roles.ToListAsync();

            var FilteredUsers = await _userServices.getAllUsersByRole(param.roleName, param.departmentId,param.Search);
            var allUsers = await _userServices.getAllUsersByRole(roleName : param.roleName);
            var count = allUsers.Count();
            //var users = await _userManager.Users.ToListAsync();
            var mappedEmps = mapper.Map<IEnumerable<ApplicationUser>, IEnumerable<ApplicationUserVM>>(FilteredUsers);


            // Pass data for dropdowns
            ViewBag.Roles = new SelectList(roles, "Name", "Name");
            ViewBag.Departments = new SelectList(departments, "Id", "Department_Name");

            var pageRes = new empsPageReuslt
            {
                Data = (List<ApplicationUserVM>)mappedEmps,
                PageNumber = param.pageNumber,
                PageSize = param.PageSize,
                TotalItems = count,
                search = param.Search,
                roleName = param.roleName,
                departmentId = param.departmentId,
            };
            return View(pageRes);
        }
    }
}
