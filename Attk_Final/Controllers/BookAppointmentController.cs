using Microsoft.AspNetCore.Mvc;
using Attk_Final.Models;
using Attk_Final.Repository;
using System;
using System.Collections.Generic;

namespace Attk_Final.Controllers
{
    public class BookAppointmentController : Controller
    {
        private readonly IBookAppointmentRepository _bookAppointmentRepository;

        public BookAppointmentController(IBookAppointmentRepository bookAppointmentRepository)
        {
            _bookAppointmentRepository = bookAppointmentRepository;
        }

        // Appointment Booking Form
        public IActionResult Index()
        {
            var patientId = 1;

            var model = new BookAppointmentViewModel
            {
                PatientId = patientId,  
                Departments = _bookAppointmentRepository.GetDepartments(),
                Patients = _bookAppointmentRepository.GetPatients() 
            };

            return View(model);
        }



        // AJAX: Get Doctors by Department
        [HttpGet]
        public JsonResult GetDoctorsByDepartment(int departmentId)
        {
            Console.WriteLine("Hello, World!", departmentId);
            var doctors = _bookAppointmentRepository.GetDoctorsByDepartment(departmentId);
            return Json(doctors);
        }

        // POST: Save Appointment
        [HttpPost]
        public IActionResult SaveAppointment(BookAppointmentViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var tokenNumber = _bookAppointmentRepository.GetNextToken(model.DoctorId);
                    model.TokenNumber = tokenNumber;
                    model.AppointmentDate = DateTime.Today;

                    _bookAppointmentRepository.SaveAppointment(model);

                    TempData["SuccessMessage"] = "Appointment booked successfully!";
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = $"Error: {ex.Message}";
                }
            }

            // Reload dropdowns if validation fails
            model.Departments = _bookAppointmentRepository.GetDepartments();
            model.Doctors = model.DepartmentId > 0
                ? _bookAppointmentRepository.GetDoctorsByDepartment(model.DepartmentId)
                : new List<Doctor>();

            return View("Index", model);
        }

    }
}
