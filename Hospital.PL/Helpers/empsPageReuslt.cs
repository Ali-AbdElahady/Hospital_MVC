using Hospital.PL.Areas.Admin.Models;

namespace Hospital.PL.Helpers
{
    public class empsPageReuslt : PagedReuslt<ApplicationUserVM>
    {
        public int? departmentId { get; set; }
        public string? roleName { get; set; }
        public string? search {  get; set; }
    }
}
