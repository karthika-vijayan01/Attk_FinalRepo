using System.Data.SqlClient;
using System.Data;
using Attk_Final.Models;
using ATTK_Project.Models;

namespace Attk_Final.Repository
{
    public class ReceptionistRepository : IReceptionistRepository
    {
        private readonly string connectionString;

        public ReceptionistRepository(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("connectionStringMVC");
        }

        public List<Patient> GetAllPatients()
        {
            var patients = new List<Patient>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("GetAllPatients", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    patients.Add(new Patient
                    {
                        PatientId = Convert.ToInt32(reader["PatientId"]),
                        PatientName = reader["PatientName"].ToString(),
                        DOB = Convert.ToDateTime(reader["DOB"]),
                        Age = Convert.ToInt32(reader["Age"]),
                        Gender = reader["Gender"].ToString(),
                        BloodGroup = reader["BloodGroup"].ToString(),
                        PhoneNumber = reader["PhoneNumber"].ToString(),
                        Address = reader["Address"].ToString(),
                        Email = reader["Email"].ToString(),
                        DateOfRegistration = Convert.ToDateTime(reader["DateOfRegistration"]),
                        Status = Convert.ToBoolean(reader["Status"])
                    });
                }
            }
            return patients;
        }

        public void AddPatient(Patient patient)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("AddPatient", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@PatientName", patient.PatientName);
                cmd.Parameters.AddWithValue("@DOB", patient.DOB);
                cmd.Parameters.AddWithValue("@Gender", patient.Gender);
                cmd.Parameters.AddWithValue("@BloodGroup", patient.BloodGroup);
                cmd.Parameters.AddWithValue("@PhoneNumber", patient.PhoneNumber);
                cmd.Parameters.AddWithValue("@Address", patient.Address);
                cmd.Parameters.AddWithValue("@Email", patient.Email);
                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void UpdatePatient(Patient patient)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("UpdatePatient", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@PatientId", patient.PatientId);
                cmd.Parameters.AddWithValue("@PhoneNumber", patient.PhoneNumber);
                cmd.Parameters.AddWithValue("@Address", patient.Address);
                cmd.Parameters.AddWithValue("@Email", patient.Email);
                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void TogglePatientStatus(int patientId, bool status)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("TogglePatientStatus", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@PatientId", patientId);
                cmd.Parameters.AddWithValue("@Status", status);
                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public Patient GetPatientById(int id)
        {
            Patient patient = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("GetPatientById", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@PatientId", id);

                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    patient = new Patient
                    {
                        PatientId = Convert.ToInt32(reader["PatientId"]),
                        PatientName = reader["PatientName"].ToString(),
                        DOB = Convert.ToDateTime(reader["DOB"]),
                        Gender = reader["Gender"].ToString(),
                        BloodGroup = reader["BloodGroup"].ToString(),
                        PhoneNumber = reader["PhoneNumber"].ToString(),
                        Address = reader["Address"].ToString(),
                        Email = reader["Email"].ToString(),
                        DateOfRegistration = Convert.ToDateTime(reader["DateOfRegistration"]),
                        Status = Convert.ToBoolean(reader["Status"]),
                        Age = Convert.ToInt32(reader["Age"]) 
                    };
                }
            }
            return patient;
        }

    }

}