using Hospital.BLL.Interfaces;
using Hospital.BLL.Repositories;
using Hospital.BLL.Specification.AppointmentSpecs;
using Hospital.BLL.Specification.DepartmentSpecs;
using Hospital.DAL.Entites;
using Hospital.PL.Areas.Admin.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

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
            ViewBag.Patients = _userManager.GetUsersInRoleAsync(WebSiteRoles.WebSite_Patient);
            ViewBag.Doctors = _userManager.GetUsersInRoleAsync(WebSiteRoles.WebSite_Doctor);
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
    }
}
