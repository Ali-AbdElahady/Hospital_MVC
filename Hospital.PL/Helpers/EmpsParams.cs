using Hospital.BLL.Specification;

namespace Hospital.PL.Helpers
{
    public class EmpsParams : EntitySpecParams
    {
        public int? departmentId { get; set; }
        public string? roleName { get; set; } = "Doctor";
    }
}
