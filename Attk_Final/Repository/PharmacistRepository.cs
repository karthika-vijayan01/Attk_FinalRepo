using Attk_Final.Models;
using System.Data.SqlClient;
using System.Data;

namespace Attk_Final.Repository
{
    public class PharmacistRepository : IPharmacistRepository
    {
        private readonly string connectionString;

        public PharmacistRepository(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("connectionStringMVC");
        }


        public List<MedicinePrescriptionModel> GetMedicinePrescriptions()
        {
            var prescriptions = new List<MedicinePrescriptionModel>();

            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand("GetMedicinePrescription", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            prescriptions.Add(new MedicinePrescriptionModel
                            {
                                AppointmentId = Convert.ToInt32(reader["AppointmentId"]),
                                PatientName = reader["PatientName"].ToString(),
                                Age = Convert.ToInt32(reader["Age"]),
                                Medicine = reader["Medicine"].ToString(),
                                DoctorName = reader["DoctorName"].ToString()
                            });
                        }
                    }
                }
            }

            return prescriptions;
        }



        public AppointmentDetailsModel GetAppointmentDetails(int? appointmentId)
        {
            var appointmentDetails = new AppointmentDetailsModel();

            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand("GetAppointmentDetails", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@AppointmentId", appointmentId);
                    connection.Open();

                    using (var reader = command.ExecuteReader())
                    {
                        // Read appointment details
                        if (reader.Read())
                        {
                            appointmentDetails.AppointmentId = Convert.ToInt32(reader["AppointmentId"]);
                            appointmentDetails.AppointmentDate = Convert.ToDateTime(reader["AppointmentDate"]);
                            appointmentDetails.PatientName = reader["PatientName"].ToString();
                            appointmentDetails.PhoneNumber = reader["PhoneNumber"].ToString();
                            appointmentDetails.Email = reader["Email"].ToString();
                            appointmentDetails.DoctorName = reader["DoctorName"].ToString();
                        }

                        // Move to medicine details result set
                        if (reader.NextResult())
                        {
                            while (reader.Read())
                            {
                                appointmentDetails.Medicines.Add(new MedicineDetailModel
                                {
                                    SerialNo = Convert.ToInt32(reader["SerialNo"]),
                                    MedicineName = reader["MedicineName"].ToString(),
                                    Quantity = Convert.ToInt32(reader["Quantity"]),
                                    Dosage = reader["Dosage"].ToString(),
                                    Duration = reader["Duration"].ToString(),
                                    Frequency = reader["Frequency"].ToString(),
                                    CreatedDate = Convert.ToDateTime(reader["CreatedDate"])
                                });
                            }
                        }
                    }
                }
            }

            return appointmentDetails;
        }




        public PrescriptionDetailModel GetBillDetails(int? appointmentId)
        {
            if (appointmentId == null) throw new ArgumentNullException(nameof(appointmentId));

            var billDetails = new PrescriptionDetailModel();

            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand("GetBillDetailsByAppointment", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@AppointmentId", appointmentId);
                    connection.Open();

                    using (var reader = command.ExecuteReader())
                    {
                        // Fetch general appointment details
                        if (reader.Read())
                        {
                            billDetails.AppointmentId = Convert.ToInt32(reader["AppointmentId"]);
                            billDetails.PatientName = reader["PatientName"].ToString();
                            billDetails.DoctorName = reader["DoctorName"].ToString();
                            billDetails.CreatedDate = Convert.ToDateTime(reader["CreatedDate"]);
                        }

                        // Fetch medicine details
                        if (reader.NextResult())
                        {
                            while (reader.Read())
                            {
                                billDetails.MedicineDetails.Add(new BillDetailsModel
                                {
                                    MedicineName = reader["MedicineName"].ToString(),
                                    Quantity = Convert.ToInt32(reader["Quantity"]),
                                    PricePerStrip = Convert.ToDecimal(reader["PricePerUnit"]),
                                    TotalAmount = Convert.ToDecimal(reader["TotalAmount"]),
                                    GST = Convert.ToDecimal(reader["GST"]),
                                });
                            }
                        }
                    }
                }
            }

            // Calculate grand total
            billDetails.GrandTotal = billDetails.MedicineDetails.Sum(m => m.TotalAmount + m.GST);

            // Delete bill records after fetching the details
            DeleteBillRecord(appointmentId);

            return billDetails;
        }

        /// <summary>
        /// Deletes bill-related records for a specific appointment.
        /// </summary>
        /// <param name="appointmentId">The ID of the appointment whose bill records are to be deleted.</param>
        private void DeleteBillRecord(int? appointmentId)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand("DeleteBillByAppointment", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@AppointmentId", appointmentId);
                    connection.Open();

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}