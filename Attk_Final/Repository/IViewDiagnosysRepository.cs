using Attk_Final.Models;

namespace Attk_Final.Repository
{
    public interface IViewDiagnosysRepository
    { // Method to insert diagnosis data into the database
        public void InsertDiagnosis(ViewDiagnosys history);

        // Optionally, add a method to retrieve diagnosis history for a given patient
        public IEnumerable<ViewDiagnosys> GetDiagnosysByPatientId(int? appointmentId);


    }
}
