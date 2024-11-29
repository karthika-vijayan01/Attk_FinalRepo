using Attk_Final.Models;
using System.Collections.Generic;

namespace Attk_Final.Repository
{
    public interface IViewBookRepository
    {
        IEnumerable<AppointmentViewModel> GetAppointmentsByDoctorId(int? doctorId);
        //object GetAppointmentsByDoctorId();
    }
}
