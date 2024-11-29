
using System.Data.SqlClient;
using System.Data;
using Attk_Final.Models;

namespace Attk_Final.Repository
{
    public class ViewDiagnosysRepository : IViewDiagnosysRepository
    {
        private readonly string _connectionString;

        // Constructor to initialize the connection string
        public ViewDiagnosysRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("connectionStringMVC");
        }

        // Method to insert diagnosis data into the database
        public void InsertDiagnosis(ViewDiagnosys history)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                using (SqlCommand cmd = new SqlCommand("InsertMainDiagnosys", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Add parameters
                    cmd.Parameters.AddWithValue("@MainPrescriptionId", history.MainPrescriptionId);
                    cmd.Parameters.AddWithValue("@Symptoms", history.Symptoms ?? string.Empty);
                    cmd.Parameters.AddWithValue("@Diagnosis", history.Diagnosis ?? string.Empty);
                    cmd.Parameters.AddWithValue("@TreatmentPlan", history.TreatmentPlan ?? string.Empty);
                    cmd.Parameters.AddWithValue("@AppointmentId", history.AppointmentId);
                    cmd.Parameters.AddWithValue("@CreatedDate", history.CreatedDate);
                    cmd.Parameters.AddWithValue("@CreatedBy", history.CreatedBy);

                    connection.Open();

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error inserting diagnosis: {ex.Message}");
                throw;
            }
        }

        // Method to get diagnosis history by AppointmentId
        public IEnumerable<ViewDiagnosys> GetDiagnosysByPatientId(int? appointmentId)
        {
            var historyList = new List<ViewDiagnosys>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                using (SqlCommand cmd = new SqlCommand("GetDiagnosysByAppointmentId", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@AppointmentId", appointmentId);

                    connection.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var history = new ViewDiagnosys
                            {
                                MainPrescriptionId = Convert.ToInt32(reader["MainPrescriptionId"]),
                                Symptoms = reader["Symptoms"].ToString(),
                                Diagnosis = reader["Diagnosis"].ToString(),
                                TreatmentPlan = reader["TreatmentPlan"].ToString(),
                                AppointmentId = Convert.ToInt32(reader["AppointmentId"]),
                                CreatedDate = Convert.ToDateTime(reader["CreatedDate"]),
                                CreatedBy = Convert.ToInt32(reader["CreatedBy"])
                            };

                            historyList.Add(history);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching diagnosis history: {ex.Message}");
                throw;
            }

            return historyList;
        }
    }

}


