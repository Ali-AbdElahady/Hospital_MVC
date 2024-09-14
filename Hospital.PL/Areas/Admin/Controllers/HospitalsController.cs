using DemoMvcAgain.BLL.Specifications;
using Hospital.BLL.Interfaces;
using Hospital.BLL.Specification;
using Hospital.DAL.Entites;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;
using Helpers = Hospital.PL.Helpers;
namespace Hospital.PL.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Hospitals")]
    [Authorize(Roles = "Admin")]
    public class HospitalsController : Controller
    {
        private readonly BLL.Interfaces.IUnitOfWork unitOfWork;
        private readonly ILogger<HospitalsController> logger;

        public HospitalsController(IUnitOfWork unitOfWork,ILogger<HospitalsController> logger)
        {
            this.unitOfWork = unitOfWork;
            this.logger = logger;
        }
        [Route("")]
        [Route("Index")]
        public async Task<IActionResult> Index(EntitySpecParams parms)
        {
            var spec = new HospitalsSpecifications(parms);
            var hospitals = await unitOfWork.GenerateGenericRepo<HospitalEntity>().GetAllWithSpecAsync(spec);
            var CountSpec = new HospitalsWithFilterationForCountAsync(parms);
            var Count = await unitOfWork.GenerateGenericRepo<HospitalEntity>().GetCountWithSpecAsync(CountSpec);
            var pageData = new Helpers.PagedReuslt<HospitalEntity>
            {
                Data = (List<HospitalEntity>) hospitals,
                PageSize = parms.PageSize,
                TotalItems = Count,
                PageNumber = parms.pageNumber

            };
            logger.LogInformation($"Retrieved hospitals: {hospitals.Count()} items.");
            return View(pageData);
        }
        [Route("Create")]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [Route("Create")]
        [HttpPost]
        public async Task<ActionResult> Create(HospitalEntity model)
        {
            if (!ModelState.IsValid)
            {
                return View(model); 
            }

            await unitOfWork.GenerateGenericRepo<HospitalEntity>().AddAsync(model);
            await unitOfWork.CompleteAsync(); 

            return RedirectToAction(nameof(Index)); 
        }
        [Route("Details")]
        [HttpGet]
        public async Task<ActionResult> Details(int? id, string viewName = "Details")
        {
            if(id is null) return BadRequest();
            var hospital = await unitOfWork.GenerateGenericRepo<HospitalEntity>().GetByIdAsync(id.Value);
            if (hospital is null) return NotFound();
            return View(viewName,hospital);
        }
        [Route("Edit")]
        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            return await Details(id, "Edit");
        }
        [Route("Edit")]
        [HttpPost]
        public async Task<IActionResult> Edit(HospitalEntity hospital)
        {
            unitOfWork.GenerateGenericRepo<HospitalEntity>().Update(hospital);
            await unitOfWork.CompleteAsync(); // Ensure the changes are saved

            return RedirectToAction(nameof(Index));
        }
        [Route("Delete")]
        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            return await Details(id, "Delete");
        }
        [Route("Delete")]
        [HttpPost]
        public async Task<IActionResult> Delete([FromRoute] int id, int Id)
        {
            if (Id != id) return BadRequest();
            var hospital = await unitOfWork.GenerateGenericRepo<HospitalEntity>().GetByIdAsync(Id);
            unitOfWork.GenerateGenericRepo<HospitalEntity>().Delete(hospital);
            await unitOfWork.CompleteAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
