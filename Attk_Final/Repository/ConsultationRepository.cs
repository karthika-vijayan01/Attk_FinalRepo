//using Attk_Final.Models;
//using System.Data.SqlClient;
//using System.Data;
//using Microsoft.Extensions.Configuration;

//namespace Attk_Final.Repository
//{
//    public class ConsultationRepository : IConsultationRepository
//    {
//        private readonly string _connectionString;

//        // Constructor to initialize the connection string from configuration
//        public ConsultationRepository(IConfiguration configuration)
//        {
//            _connectionString = configuration.GetConnectionString("connectionStringMVC");
//        }

//        // Add Doctor method to insert a doctor into the database
//        public void AddDoctor(consultation doctor)
//        {
//            using (SqlConnection connection = new SqlConnection(_connectionString))
//            {
//                SqlCommand command = new SqlCommand("AddDoctorss", connection);
//                command.CommandType = CommandType.StoredProcedure;

//                command.Parameters.AddWithValue("@StaffId", doctor.StaffId);
//                command.Parameters.AddWithValue("@ConsultationFee", doctor.ConsultationFee);
//                command.Parameters.AddWithValue("@SpecializationId", doctor.SpecializationName); // Pass the SpecializationId here

//                connection.Open();
//                command.ExecuteNonQuery();
//            }
//        }


//        // Method to get all specializations from the database
//        public List<dynamic> GetSpecializations()
//        {
//            List<dynamic> specializations = new List<dynamic>();

//            using (SqlConnection connection = new SqlConnection(_connectionString))
//            {
//                SqlCommand command = new SqlCommand("GetAllSpecializations", connection);
//                connection.Open();
//                SqlDataReader reader = command.ExecuteReader();

//                while (reader.Read())
//                {
//                    specializations.Add(new
//                    {
//                        SpecializationId = reader.GetInt32(0),
//                        SpecializationName = reader.GetString(1)
//                    });
//                }
//            }

//            return specializations;
//        }
//    }
//}
