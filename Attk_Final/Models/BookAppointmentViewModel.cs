using ATTK_Project.Models;

namespace Attk_Final.Models
{
    public class BookAppointmentViewModel
    {
        public int PatientId { get; set; }
        public int DepartmentId { get; set; }
        public int DoctorId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public int TokenNumber { get; set; }

        public List<Department> Departments { get; set; }
        public List<Doctor> Doctors { get; set; }
        public List<Patient> Patients { get; set; }
    }

    public class Department
    {
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
    }

   
    
}
