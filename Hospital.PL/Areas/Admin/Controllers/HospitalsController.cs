using DemoMvcAgain.BLL.Specifications;
using Hospital.BLL.Interfaces;
using Hospital.BLL.Specification;
using Hospital.DAL.Entites;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.PL.Areas.Admin.Controllers
{
    public class HospitalsController : Controller
    {
        private readonly BLL.Interfaces.IUnitOfWork unitOfWork;
        private readonly ILogger logger;

        public HospitalsController(IUnitOfWork unitOfWork,ILogger logger)
        {
            this.unitOfWork = unitOfWork;
            this.logger = logger;
        }
        public async Task<IActionResult> Index(EntitySpecParams parms)
        {
            var spec = new HospitalsSpecifications(parms);
            var hospitals = await unitOfWork.GenerateGenericRepo<HospitalEntity>().GetAllWithSpecAsync(spec);
            logger.LogInformation($"hospitals {hospitals}");
            return View(hospitals);
        }
    }
}
