using Attk_Final.Models;
using Attk_Final.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Attk_Final.Controllers
{
    /*
    public class ViewDiagnosysController : Controller
    {
        private readonly IViewBookRepository _bookRepository;
        private readonly IViewDiagnosysRepository _diagnosysRepository;

        // Constructor for dependency injection
        public ViewDiagnosysController(IViewBookRepository bookRepository, IViewDiagnosysRepository diagnosysRepository)
        {
            _bookRepository = bookRepository;
            _diagnosysRepository = diagnosysRepository;
        }

        private readonly IViewDiagnosysRepository _repository;

            // Inject the repository in the constructor
            public ViewDiagnosysController(IViewDiagnosysRepository repository)
            {
                _repository = repository;
            }

            // POST: api/ViewDiagnosys
            [HttpPost("InsertDiagnosis")]
            public IActionResult InsertDiagnosis([FromBody] ViewDiagnosys history)
            {
                try
                {
                    if (history == null)
                    {
                        return BadRequest("Invalid data.");
                    }

                    // Call the repository method to insert the diagnosis
                    _repository.InsertDiagnosis(history);

                    return Ok("Diagnosis inserted successfully.");
                }
                catch (Exception ex)
                {
                    // Log the error and return a failure response
                    Console.WriteLine($"Error: {ex.Message}");
                    return StatusCode(500, "An error occurred while inserting the diagnosis.");
                }
            }
        

        // POST: Submit Diagnosis
            [HttpPost]
              public IActionResult SubmitDiagnosis(ViewDiagnosys history)
              {
                  if (ModelState.IsValid)
                  {
                      // Save the diagnosis details in the database
                      _diagnosysRepository.InsertDiagnosis(history);

                      // Redirect to the View Diagnosis page for the patient
                      return RedirectToAction("ViewDiagnosys", new { Id = history.AppointmentId });
                  }

                  return View("StartDiagnosis", history);
              }
        
        // GET: View Patient's Diagnosis History
        public IActionResult ViewDiagnosys(int id)
        {
            // Fetch the patient's diagnosis history by AppointmentId
            var historyList = _diagnosysRepository.GetDiagnosysByPatientId(id);

            if (!historyList.Any())
            {
                return NotFound("No diagnosis history found for this appointment.");
            }

            // Pass the history list to the view
            return View(historyList);
        }
    }
    */
    public class ViewDiagnosysController : Controller
    {
        private readonly IViewBookRepository _bookRepository;
        private readonly IViewDiagnosysRepository _diagnosysRepository;

        public ViewDiagnosysController(IViewBookRepository bookRepository, IViewDiagnosysRepository diagnosysRepository)
        {
            _bookRepository = bookRepository;
            _diagnosysRepository = diagnosysRepository;
        }


        // GET: Start Diagnosis Form
        public IActionResult StartDiagnosis(int id = 1)
        {
            // Fetch the appointment details using B_Id
            var appointment = _bookRepository.GetAppointmentsByDoctorId(id).FirstOrDefault(b => b.AppointmentId == id);
            if (appointment == null)
            {
                return NotFound();
            }

            // Pass the appointment details to the form
            var history = new ViewDiagnosys
            {
                MainPrescriptionId = appointment.AppointmentId,
                AppointmentId = appointment.AppointmentId
            };

            return View(history);
        }

        //        POST: Submit Diagnosis
        [HttpPost]
        public IActionResult SubmitDiagnosis(ViewDiagnosys history)
        {
            if (ModelState.IsValid)
            {
                // Save the diagnosis details in the history table
                _diagnosysRepository.InsertDiagnosis(history);

                // Redirect to the history view for the patient
                return RedirectToAction("ViewDiagnosys", new { Id = history.MainPrescriptionId });
            }

            return View("ViewDiagnosys", history);
        }


        // GET: View Patient's History
        public IActionResult ViewDiagnosys(int Id)
        {
            // Fetch the patient's diagnosis history
            var historyList = _diagnosysRepository.GetDiagnosysByPatientId(Id);

            // Pass the history list to the view
            return View(historyList);
        }
    }


    //------------------------------------------------------------------


    public class MedicinePrescriptionController : ControllerBase
    {
        private readonly IMedicinePrescriptionRepository _repository;

        public MedicinePrescriptionController(IMedicinePrescriptionRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public async Task<IActionResult> AddMedicinePrescription([FromBody] AddMedicineClass medicinePrescription)
        {
            if (medicinePrescription == null)
                return BadRequest("Invalid data.");

            var result = _repository.InsertMedicinePrescription(medicinePrescription);

            if (result > 0)
                return Ok("Medicine Prescription added successfully.");
            else
                return StatusCode(500, "An error occurred while adding the record.");
        }
    }

}

