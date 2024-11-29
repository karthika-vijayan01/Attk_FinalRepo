using Attk_Final.Models;

namespace Attk_Final.Repository
{
    public interface IPharmacistRepository
    {
        List<MedicinePrescriptionModel> GetMedicinePrescriptions();

        public AppointmentDetailsModel GetAppointmentDetails(int? appointmentId);

        public PrescriptionDetailModel GetBillDetails(int? appointmentId);
    }
}
