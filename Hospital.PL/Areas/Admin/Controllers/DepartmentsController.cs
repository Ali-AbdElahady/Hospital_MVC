using AutoMapper;
using Hospital.BLL.Interfaces;
using Hospital.BLL.Specification;
using Hospital.DAL.Entites;
using Hospital.PL.Areas.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Hospital.PL.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Departments")]
    [Authorize(Roles = "Admin")]
    public class DepartmentsController : Controller
    {
        private readonly BLL.Interfaces.IUnitOfWork unitOfWork;
        private readonly ILogger<DepartmentsController> logger;
        private readonly IMapper mapper;

        public DepartmentsController(IUnitOfWork unitOfWork, ILogger<DepartmentsController> logger, IMapper Mapper)
        {
            this.unitOfWork = unitOfWork;
            this.logger = logger;
            mapper = Mapper;
        }
        [Route("")]
        [Route("Index")]
        public async Task<IActionResult> Index(DepartmentSpecParams parms)
        {
            var spec = new DepartmentsSpecifications(parms);
            var departments = await unitOfWork.GenerateGenericRepo<Department>().GetAllWithSpecAsync(spec);
            var MappedDeps = mapper.Map<IReadOnlyList<Department>,IReadOnlyList<DepartmentVM>>(departments);
            var CountSpec = new DepartmentsWithFilterationForCountAsync(parms);
            var Count = await unitOfWork.GenerateGenericRepo<Department>().GetCountWithSpecAsync(CountSpec);
            var pageData = new Helpers.PagedReuslt<DepartmentVM>
            {
                Data = (List<DepartmentVM>)MappedDeps,
                PageSize = parms.PageSize,
                TotalItems = Count,
                PageNumber = parms.pageNumber

            };
            logger.LogInformation($"Retrieved departments: {departments.Count()} items.");
            return View(pageData);
        }
        [Route("Create")]
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.hospitals = await unitOfWork.GenerateGenericRepo<HospitalEntity>().GetAllAsync();
            return View();
        }
        [Route("Create")]
        [HttpPost]
        public async Task<ActionResult> Create(DepartmentVM model)
        {
            if (string.IsNullOrEmpty(model.Department_Name) || model.Hospital_Id == null)
            {
                return RedirectToAction(nameof(Create));
            }
            var mappedDep = mapper.Map<DepartmentVM, Department>(model);
            await unitOfWork.GenerateGenericRepo<Department>().AddAsync(mappedDep);
            await unitOfWork.CompleteAsync();

            return RedirectToAction(nameof(Index));
        }
        [Route("Details")]
        [HttpGet]
        public async Task<ActionResult> Details(int? id, string viewName = "Details")
        {
            if (id is null) return BadRequest();
            var spec = new DepartmentsSpecifications(id.Value);
            var Department = await unitOfWork.GenerateGenericRepo<Department>().GetByIdWithSpecAsync(spec);
            if (Department is null) return NotFound();
            var mappedDep = mapper.Map<Department, DepartmentVM>(Department);
            return View(viewName, mappedDep);
        }
        [Route("Edit")]
        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            ViewBag.Hospitals = await unitOfWork.GenerateGenericRepo<HospitalEntity>().GetAllAsync();
            return await Details(id, "Edit");
        }
        [Route("Edit")]
        [HttpPost]
        public async Task<IActionResult> Edit(DepartmentVM department)
        {
            var mappedDep = mapper.Map<DepartmentVM, Department>(department);
            unitOfWork.GenerateGenericRepo<Department>().Update(mappedDep);
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
            var hospital = await unitOfWork.GenerateGenericRepo<Department>().GetByIdAsync(Id);
            unitOfWork.GenerateGenericRepo<Department>().Delete(hospital);
            await unitOfWork.CompleteAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
