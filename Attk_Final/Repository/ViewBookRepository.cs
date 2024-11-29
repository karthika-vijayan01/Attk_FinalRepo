using Attk_Final.Models;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
namespace Attk_Final.Repository
{
    public class ViewBookRepository : IViewBookRepository
    {
        private readonly string connectionString;
        public ViewBookRepository(IConfiguration configuration)
        {
            //Iconfiguration is a built-in interface that provides access to configuration settings,
            //such as value stored in appsetings.json
            connectionString = configuration.GetConnectionString("connectionStringMVC");
        }



        public IEnumerable<AppointmentViewModel> GetAppointmentsByDoctorId(int? doctorId)
        {
            var appointmentList = new List<AppointmentViewModel>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_ViewAppointmentsByDoctorId", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@DoctorId", doctorId);

                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var appointment = new AppointmentViewModel
                    {
                        AppointmentId = Convert.ToInt32(reader["AppointmentId"]),
                        TokenNumber = Convert.ToInt32(reader["Token"]),
                        AppointmentDate = Convert.ToDateTime(reader["DOA"]),
                        DoctorId = Convert.ToInt32(reader["Doctor_Id"]),
                        PatientId = Convert.ToInt32(reader["PatientId"]),
                        PatientName = reader["P_Name"].ToString(),
                        BloodGroup = reader["Blood_Group"].ToString(),
                        Gender = reader["Gender"].ToString(),
                        PhoneNumber = reader["P_Phone"].ToString()
                    };

                    appointmentList.Add(appointment);
                }
            }

            return appointmentList;
        }
    }
}
    

