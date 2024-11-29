using ATTK_Project.Models;

namespace Attk_Final.Repository
{
    public interface IReceptionistRepository
    {
        List<Patient> GetAllPatients();
        void AddPatient(Patient patient);
        void UpdatePatient(Patient patient);
        void TogglePatientStatus(int patientId, bool status);
        Patient GetPatientById(int id);
    }
}
