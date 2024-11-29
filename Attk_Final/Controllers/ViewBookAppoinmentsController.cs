using Attk_Final.Models;
using Attk_Final.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Attk_Final.Controllers
{
    public class ViewBookAppointmentsController : Controller
    {
        private readonly IViewBookRepository _repository;

        public ViewBookAppointmentsController(IViewBookRepository repository)
        {
            _repository = repository;
        }

        //[HttpGet("doctor/appointments/{doctorid}")]
        [HttpGet]
        public IActionResult GetAppointmentsByDoctorId(int? Id)
        {
            if (Id <= 0)
            {
                ViewBag.Message = "Invalid Doctor ID.";
                return View("NoAppointments");
            }

            var appointments = _repository.GetAppointmentsByDoctorId(Id);

            if (!appointments.Any())
            {
                ViewBag.Message = $"No appointments found for Doctor ID: {Id}.";
                return View("NoAppointments");
            }

            //return View("Appointments", appointments);
            return View(appointments);
        }

    }
}
