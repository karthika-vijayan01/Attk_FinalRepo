using Attk_Final.Models;
using Attk_Final.Repository;
using ATTK_Project.Models;
using Microsoft.AspNetCore.Mvc;

namespace Attk_Final.Controllers
{
    public class ReceptionistController : Controller
    {
        private readonly IReceptionistRepository receptionistRepository;

        public ReceptionistController(IReceptionistRepository _receptionistRepository)
        {
            receptionistRepository = _receptionistRepository;
        }

        public IActionResult Index(string query)
        {
            
            var patients = receptionistRepository.GetAllPatients();

            if (!string.IsNullOrWhiteSpace(query))
            {
                patients = patients.Where(p =>
                    (!string.IsNullOrEmpty(p.PatientName) && p.PatientName.Contains(query, StringComparison.OrdinalIgnoreCase)) ||
                    (!string.IsNullOrEmpty(p.PhoneNumber) && p.PhoneNumber.Contains(query)) ||
                    p.PatientId.ToString().Contains(query)
                ).ToList();

                ViewData["IsSearchResult"] = true; 

                if (!patients.Any())
                {
                    ViewData["NoResultsMessage"] = "No patient records found matching your search query.";
                }
            }
            else
            {
                ViewData["IsSearchResult"] = false; 
                ViewData["NoResultsMessage"] = null; 
            }

            ViewData["Query"] = query;
            return View(patients);
        }


        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Patient patient)
        {
            if (ModelState.IsValid)
            {
                patient.DateOfRegistration = DateTime.Today;

                if (patient.DOB != null)
                {
                    patient.Age = DateTime.Now.Year - patient.DOB.Year;
                    if (DateTime.Now.DayOfYear < patient.DOB.DayOfYear)
                        patient.Age--;
                }

                receptionistRepository.AddPatient(patient);
                return RedirectToAction("Index");
            }

            return View(patient);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var patient = receptionistRepository.GetPatientById(id);
            if (patient == null || !patient.Status)
            {
                return NotFound();
            }
            return View(patient);
        }

        [HttpPost]
        public IActionResult Edit(Patient patient)
        {
            if (ModelState.IsValid)
            {
                receptionistRepository.UpdatePatient(patient);
                return RedirectToAction("Index");
            }
            return View(patient);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var patient = receptionistRepository.GetPatientById(id);
            if (patient == null)
            {
                return NotFound();
            }
            return View(patient);
        }

        [HttpPost]
        public IActionResult ToggleStatus(int id, bool status)
        {
            receptionistRepository.TogglePatientStatus(id, status);
            return RedirectToAction("Index");
        }



    }


}