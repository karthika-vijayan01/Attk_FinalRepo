using System.Data.SqlClient;
using System.Data;
using Attk_Final.Models;

namespace Attk_Final.Repository
{
    public class MedicinePrescriptionRepository : IMedicinePrescriptionRepository
    {
         private readonly string connectionstring;

            public MedicinePrescriptionRepository(IConfiguration configuration)
            {
                connectionstring = configuration.GetConnectionString("connectionStringMVC");
            }

        public int InsertMedicinePrescription(AddMedicineClass medicinePrescription)
        {
            int result;

            // Define the connection string (make sure it's initialized properly in your configuration)
            using (SqlConnection connection = new SqlConnection(connectionstring))
            {
                // Define the SQL command
                using (SqlCommand command = new SqlCommand("InsertMedicinePrescription", connection))
                {
                    // Specify that this command is a stored procedure
                    command.CommandType = CommandType.StoredProcedure;

                    // Add parameters to the command
                    command.Parameters.AddWithValue("@AppointmentId", medicinePrescription.AppointmentId);
                    command.Parameters.AddWithValue("@MedicineId", medicinePrescription.MedicineId);
                    command.Parameters.AddWithValue("@Quantity", medicinePrescription.Quantity);
                    command.Parameters.AddWithValue("@Dosage", medicinePrescription.Dosage);
                    command.Parameters.AddWithValue("@Duration", medicinePrescription.Duration);
                    command.Parameters.AddWithValue("@Frequency", medicinePrescription.Frequency);
                    command.Parameters.AddWithValue("@IsMedicineStatus", medicinePrescription.IsMedicineStatus);
                    command.Parameters.AddWithValue("@CreatedDate", medicinePrescription.CreatedDate);
                    command.Parameters.AddWithValue("@CreatedBy", medicinePrescription.CreatedBy);

                    // Open the connection
                    connection.Open();

                    // Execute the command and get the result
                    result = command.ExecuteNonQuery();
                }
            }

            // Return the result (number of rows affected)
            return result;
        }

    }

}

