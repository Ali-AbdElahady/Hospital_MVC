namespace Hospital.DAL.Entites
{
    public class Pharmacy : BaseEntity
    {
        public string Pharmacy_Name { get; set; }
        public string? Pharmacy_Address { get; set; }
        public string? Pharmacy_Phone_Number { get; set; }
        public ICollection<ApplicationUser> Patients { get; set; } = new HashSet<ApplicationUser>();
    }
}