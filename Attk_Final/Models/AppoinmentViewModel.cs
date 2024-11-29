namespace Attk_Final.Models
{
    public class AppointmentViewModel
    {
        public int AppointmentId { get; set; }
        public int PatientId { get; set; }
        public string PatientName { get; set; }
        public DateTime AppointmentDate { get; set; }
        public int TokenNumber { get; set; }
        public int DoctorId { get; set; }
        public string Gender { get; set; }
        public string BloodGroup { get; set; }
        public string PhoneNumber { get; set; }
    }
}
