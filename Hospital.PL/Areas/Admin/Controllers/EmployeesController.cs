using AutoMapper;
using Hospital.BLL.Interfaces;
using Hospital.BLL.Repositories;
using Hospital.DAL.Entites;
using Hospital.PL.Areas.Admin.Models;
using Hospital.PL.Helpers;
using Hospital.PL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Differencing;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
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
            var empParams = new EmpsParams
            {
                roleName = param.roleName,
                departmentId = param.departmentId,
                Search = param.Search,
            };
            empParams.ApplyPagenation((param.pageNumber - 1) * param.PageSize, param.PageSize);
            var FilteredUsers = await _userServices.getAllUsersByRole(empParams);
            empParams.isPagenationOn = false;
            var allUsers = await _userServices.getAllUsersByRole(empParams);
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

        [Route("Details")]
        [HttpGet]
        public async Task<IActionResult> Details(string? id,string viewName = "Details")
        {
            if (id is null) return BadRequest();
            var emp = await _userServices.getUserById(id);
            var currentRole = await _userManager.GetRolesAsync(emp);
            if (emp is null) return NotFound();
            var mappedEmp = mapper.Map<ApplicationUser, ApplicationUserVM>(emp);
            mappedEmp.Role = currentRole.FirstOrDefault();
            return View(viewName, mappedEmp);
        }
        [Route("Create")]
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var departments = await _unitOfWork.GenerateGenericRepo<Department>().GetAllAsync();
            var Specializations = await _unitOfWork.GenerateGenericRepo<Specialization>().GetAllAsync();

            var roles = await _roleManager.Roles.ToListAsync();
            ViewBag.Roles = new SelectList(roles, "Name", "Name");
            ViewBag.Departments = new SelectList(departments, "Id", "Department_Name");
            ViewBag.Specializations = new SelectList(Specializations, "Id", "Name");
            return View();
        }
        [Route("Create")]
        [HttpPost]
        public async Task<IActionResult> Create(ApplicationUserVM empVm)
        {
            if(String.IsNullOrWhiteSpace(empVm.FName) ||
                String.IsNullOrWhiteSpace(empVm.LName) ||
                String.IsNullOrWhiteSpace(empVm.Role) ||
                empVm.Department == null || !String.IsNullOrEmpty(empVm.Id)
                ) return View(empVm);
            await _userServices.CreateUser(empVm);
            return RedirectToAction(nameof(Index));
        }
        [Route("Edit")]
        [HttpGet]
        public async Task<IActionResult> Edit(string? id)
        {
            var departments = await _unitOfWork.GenerateGenericRepo<Department>().GetAllAsync();
            var Specializations = await _unitOfWork.GenerateGenericRepo<Specialization>().GetAllAsync();

            var roles = await _roleManager.Roles.ToListAsync();
            ViewBag.Roles = new SelectList(roles, "Name", "Name");
            ViewBag.Departments = new SelectList(departments, "Id", "Department_Name");
            ViewBag.Specializations = new SelectList(Specializations, "Id", "Name");
            return await Details(id,"Edit");
        }
        [Route("Edit")]
        [HttpPost]
        public async Task<IActionResult> Edit(ApplicationUserVM emp)
        {
            var user = await _userManager.FindByIdAsync(emp.Id);
            if (user == null)
            {
                return NotFound("User not found");
            }
            var updateUserResult = await _userServices.updateUser(emp);
            if (!updateUserResult.Succeeded)
            {
                return BadRequest("Failed to update user");
            }

            return RedirectToAction(nameof(Index));
        }


    }
}
