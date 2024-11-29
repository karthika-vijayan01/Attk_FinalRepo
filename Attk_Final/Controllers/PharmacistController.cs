using Attk_Final.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Attk_Final.Controllers
{
    using Attk_Final.Models;
    using Microsoft.AspNetCore.Mvc;

    public class PharmacistController : Controller
    {
        private readonly IPharmacistRepository _pharmacistRepository;

        public PharmacistController(IPharmacistRepository pharmacistRepository)
        {
            _pharmacistRepository = pharmacistRepository;
        }

        // GET: Medicine Prescriptions
        public IActionResult MedicinePrescriptions()
        {
            var prescriptions = _pharmacistRepository.GetMedicinePrescriptions();
            return View(prescriptions);
        }

        // GET: Appointment Details
        public IActionResult AppointmentDetails(int? id)
        {
            var details = _pharmacistRepository.GetAppointmentDetails(id);
            //var prescriptions = _pharmacistRepository.GetMedicinePrescriptions(appointmentId);

            ViewBag.Prescriptions = details;
            return View(details);
        }


        public IActionResult BillDetails(int? id)
        {
            var billDetails = _pharmacistRepository.GetBillDetails(id);
            return View(billDetails);
        }

        [HttpPost]
        public IActionResult PrintBill(PrescriptionDetailModel model)
        {
            // Logic to handle printing, or redirect to a printable version of the bill
            return RedirectToAction("PrintBillView", new { appointmentId = model.AppointmentId });
        }


        //// POST: Update Medicine Status
        //[HttpPost]
        //public IActionResult UpdateMedicineStatus(int medicinePrescriptionId)
        //{
        //    _pharmacistRepository.UpdateMedicineStatus(medicinePrescriptionId);
        //    return RedirectToAction("MedicinePrescriptions");
        //}

        //// POST: Add Medicine Bill
        //[HttpPost]
        //public IActionResult AddMedicineBill(int appointmentId, decimal grandTotal, int createdBy)
        //{
        //    _pharmacistRepository.AddMedicineBill(appointmentId, grandTotal, createdBy);
        //    return RedirectToAction("BillDetails", new { appointmentId });
        //}




    }
}
