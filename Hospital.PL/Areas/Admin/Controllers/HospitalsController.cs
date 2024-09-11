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
    }
}
