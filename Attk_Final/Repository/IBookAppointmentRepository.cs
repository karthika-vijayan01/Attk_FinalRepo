using System.Collections.Generic;
using Attk_Final.Models;
using ATTK_Project.Models;

namespace Attk_Final.Repository
{
    public interface IBookAppointmentRepository
    {
        List<Department> GetDepartments();
        List<Doctor> GetDoctorsByDepartment(int departmentId);
        int GetNextToken(int doctorId);
        void SaveAppointment(BookAppointmentViewModel model);
        List<Patient> GetPatients(); 
    }
}
