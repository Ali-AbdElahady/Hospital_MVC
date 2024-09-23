using DemoMvcAgain.BLL.Specifications;
using Hospital.BLL.Interfaces;
using Hospital.BLL.Repositories;
using Hospital.BLL.Specification.AppointmentSpecs;
using Hospital.BLL.Specification.DepartmentSpecs;
using Hospital.DAL.Entites;
using Hospital.PL.Areas.Admin.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hospital.PL.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Appointments")]
    public class AppointmentsController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;

        public AppointmentsController(UserManager<ApplicationUser> userManager,
            IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
        }
        [Route("")]
        [Route("index")]
        public async Task<IActionResult> Index(AppointmentSpecParams Params)
        {
            ViewBag.Patients = await _userManager.GetUsersInRoleAsync(WebSiteRoles.WebSite_Patient);
            ViewBag.Doctors = await _userManager.GetUsersInRoleAsync(WebSiteRoles.WebSite_Doctor);
            var spec = new AppointmentSpecification(Params);
            var Appointments = await _unitOfWork.GenerateGenericRepo<Appointment>().GetAllWithSpecAsync(spec);
            var CountSpec = new AppointmnetWithFilterationForCountAsync(Params);
            var Count = await _unitOfWork.GenerateGenericRepo<Appointment>().GetCountWithSpecAsync(CountSpec);
            var pageData = new Helpers.PagedReuslt<Appointment>
            {
                Data = Appointments,
                PageSize = Params.PageSize,
                TotalItems = Count,
                PageNumber = Params.pageNumber

            };
            return View(pageData);
        }
        



        [Route("SearchPatients")]
        [HttpGet]
        public async Task<IActionResult> SearchPatients(string searchTerm)
        {
            var patients = await _userManager.GetUsersInRoleAsync(WebSiteRoles.WebSite_Patient);

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                patients = patients.Where(p => $"{p.FName} {p.LName}".ToLower().Contains(searchTerm.ToLower())).ToList();
            }

            // Return the filtered list of patients as a JSON response
            return Json(patients.Select(p => new { id = p.Id, name = $"{p.FName} {p.LName}" }));
        }
        [Route("SearchDoctors")]
        [HttpGet]
        public async Task<IActionResult> SearchDoctors(string searchTerm)
        {
            var doctors = await _userManager.GetUsersInRoleAsync(WebSiteRoles.WebSite_Doctor);

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                doctors = doctors.Where(p => $"{p.FName} {p.LName}".ToLower().Contains(searchTerm.ToLower())).ToList();
            }

            // Return the filtered list of patients as a JSON response
            return Json(doctors.Select(p => new { id = p.Id, name = $"{p.FName} {p.LName}" }));
        }

        // Step 1: Select Hospital
        [Route("Create")]
        public async Task<IActionResult> Create()
        {
            var hospitals = await _unitOfWork.GenerateGenericRepo<HospitalEntity>().GetAllAsync();
            return View(hospitals);
        }

        // Step 2: Get Departments by Hospital
        [HttpGet]
        [Route("GetDepartments")]
        public async Task<JsonResult> GetDepartments(int hospitalId)
        {
            var Params = new DepartmentSpecParams
            {
                Hospital_Id = hospitalId,
            };
            var spec = new DepartmentsSpecifications(Params);
            var departments = await _unitOfWork.GenerateGenericRepo<Department>().GetAllWithSpecAsync(spec);
            return Json(departments);
        }

        // Step 3: Get Doctors by Department
        [HttpGet]
        [Route("GetDoctors")]
        public async Task<JsonResult> GetDoctors(int departmentId)
        {
            var doctors = await _userManager.Users.Where(U=>U.Department_ID == departmentId).ToListAsync();
            return Json(doctors);
        }

        // Step 4: Create Appointment
        [HttpPost]
        public IActionResult CreateAppointment(Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                //_context.Appointments.Add(appointment);
                //_context.SaveChanges();
                return RedirectToAction("Index"); // Redirect to a confirmation page or index
            }
            return View(appointment);
        }
    }
}
