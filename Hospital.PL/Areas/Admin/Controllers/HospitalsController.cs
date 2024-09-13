using DemoMvcAgain.BLL.Specifications;
using Hospital.BLL.Interfaces;
using Hospital.BLL.Specification;
using Hospital.DAL.Entites;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Helpers = Hospital.PL.Helpers;
namespace Hospital.PL.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Hospitals")]
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
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(HospitalEntity model)
        {
            if (!ModelState.IsValid)
            {
                return View(model); 
            }

            await unitOfWork.GenerateGenericRepo<HospitalEntity>().AddAsync(model);
            await unitOfWork.CompleteAsync(); 

            return RedirectToAction(nameof(Index)); 
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update(HospitalEntity model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

             unitOfWork.GenerateGenericRepo<HospitalEntity>().UpdateAsync(model);
            await unitOfWork.CompleteAsync(); // Ensure the changes are saved

            return RedirectToAction(nameof(Index));
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var hospital = await unitOfWork.GenerateGenericRepo<HospitalEntity>().GetByIdAsync(id);
            if (hospital == null)
            {
                return NotFound();
            }

             unitOfWork.GenerateGenericRepo<HospitalEntity>().DeleteAsync(hospital);
            await unitOfWork.CompleteAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
