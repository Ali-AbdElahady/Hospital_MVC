namespace Hospital.PL.Areas.Admin.Models
{
    public class DepartmentVM
    {
        public int Id { get; set; }
        public string Department_Name { get; set; }
        public int Hospital_Id { get; set; }
        public string Hospital_Name {  get; set; }
    }
}
