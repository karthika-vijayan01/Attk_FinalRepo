namespace Attk_Final.Models
{
    public class AddStaffViewModel
    {
        public Staff StaffDetails { get; set; }
        public decimal? ConsultationFee { get; set; } // Optional for Doctors
        public string SpecializationName { get; set; } // Optional for Doctors
        public string DepartmentName { get; set; }
        public string RoleName { get; set; }
    }

}
