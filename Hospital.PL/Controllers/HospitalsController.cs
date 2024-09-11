using Microsoft.AspNetCore.Mvc;

namespace Hospital.PL.Controllers
{
    public class HospitalsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
