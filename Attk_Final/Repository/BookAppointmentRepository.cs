using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Attk_Final.Models;
using ATTK_Project.Models;
using Microsoft.Extensions.Configuration;

namespace Attk_Final.Repository
{
    public class BookAppointmentRepository : IBookAppointmentRepository
    {
        private readonly string connectionString;

        public BookAppointmentRepository(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("connectionStringMVC");
        }

        public List<Department> GetDepartments()
        {
            var departments = new List<Department>();
            using (var connection = new SqlConnection(connectionString))
            {
                var command = new SqlCommand("SELECT DepartmentId, DepartmentName FROM Departments", connection);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        departments.Add(new Department
                        {
                            DepartmentId = Convert.ToInt32(reader["DepartmentId"]),
                            DepartmentName = reader["DepartmentName"].ToString()
                        });
                    }
                }
            }
            return departments;
        }


        public List<Doctor> GetDoctorsByDepartment(int departmentId)
        {
            var doctors = new List<Doctor>();
            using (var connection = new SqlConnection(connectionString))
            {
                var command = new SqlCommand("GetDoctorsByDepartment", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@DepartmentId", departmentId);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        doctors.Add(new Doctor
                        {
                            DoctorId = Convert.ToInt32(reader["DoctorId"]),
                            Name = reader["DoctorName"].ToString()
                        });
                    }
                }
            }
            return doctors;
        }


        public int GetNextToken(int doctorId)
        {
            int nextToken;
            using (var connection = new SqlConnection(connectionString))
            {
                var command = new SqlCommand("GetNextToken", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@DoctorId", doctorId);

                var outputParam = new SqlParameter
                {
                    ParameterName = "@NextToken",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Output
                };
                command.Parameters.Add(outputParam);

                connection.Open();
                command.ExecuteNonQuery();
                nextToken = (int)outputParam.Value;
            }
            return nextToken;
        }

        public void SaveAppointment(BookAppointmentViewModel model)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var command = new SqlCommand("AddAppointment", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@PatientId", model.PatientId);
                command.Parameters.AddWithValue("@DoctorId", model.DoctorId);
                command.Parameters.AddWithValue("@AppointmentDate", model.AppointmentDate);
                command.Parameters.AddWithValue("@TokenNumber", model.TokenNumber);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }


        public void AddAppointment(BookAppointmentViewModel model)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("AddAppointment", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@PatientId", model.PatientId);
                cmd.Parameters.AddWithValue("@DoctorId", model.DoctorId);
                cmd.Parameters.AddWithValue("@AppointmentDate", DateTime.Now);
                cmd.Parameters.AddWithValue("@TokenNumber", model.TokenNumber);
                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public List<Patient> GetPatients()
        {
            var patients = new List<Patient>();
            using (var connection = new SqlConnection(connectionString))
            {
                var command = new SqlCommand("SELECT PatientId FROM Patient", connection);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        patients.Add(new Patient
                        {
                            PatientId = Convert.ToInt32(reader["PatientId"]),
                            //PatientName = reader["PatientName"].ToString()
                        });
                    }
                }
            }
            return patients;
        }


    }
}
