//using Attk_Final.Models;
//using Attk_Final.Repository;
//using Microsoft.AspNetCore.Mvc;

//namespace Attk_Final.Controllers
//{
//    public class ConsultationController : Controller
//    {
//        private readonly IConsultationRepository _consultationRepository;

//        public ConsultationController(IConsultationRepository consultationRepository)
//        {
//            _consultationRepository = consultationRepository;
//        }

//        // Get method to load the page and populate the specialization dropdown
//        public IActionResult AddDoctor()
//        {
//            // Get specializations from the repository
//            var specializations = _consultationRepository.GetSpecializations();
//            // Pass specializations to the view
//            ViewBag.Specializations = specializations;
//            return View();
//        }

//        // Post method to handle form submission and add the doctor
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public IActionResult AddDoctor(consultation doctor)
//        {
//            if (ModelState.IsValid)
//            {
//                // Pass the SpecializationId to the repository
//                _consultationRepository.AddDoctor(doctor);

//                // Success message
//                TempData["SuccessMessage"] = "Doctor added successfully.";
//                return RedirectToAction("AddDoctor");
//            }

//            // Return to the same page with validation errors if any
//            var specializations = _consultationRepository.GetSpecializations();
//            ViewBag.Specializations = specializations;
//            return View(doctor);
//        }

//    }
//}
