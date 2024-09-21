using Hospital.BLL.Specification;

namespace Hospital.PL.Helpers
{
    public class EmpsParams : EntitySpecParams
    {
        public int? departmentId { get; set; }
        public string? roleName { get; set; } = "Doctor";
        public int Take { get; set; }
        public int Skip { get; set; }
        public bool? isPagenationOn { get; set; } = false;

        public void ApplyPagenation(int skip,int take)
        {
            isPagenationOn = true;
            Skip = skip;
            Take = take;
        }
    }
}
