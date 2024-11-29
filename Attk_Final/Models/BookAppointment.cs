//using ATTK_Project.Models;
//using System;

//namespace Attk_Final.Models
//{
//    public class BookAppointment
//    {
//        public int AppointmentId { get; set; }  // Unique identifier for each appointment
//        public int PatientId { get; set; }      // Patient ID
//        public string PatientName { get; set; } // Patient's name
//        public int DepartmentId { get; set; }   // Department ID
//        public string DepartmentName { get; set; } // Department name
//        public int DoctorId { get; set; }       // Doctor ID
//        public string DoctorName { get; set; }  // Doctor's name
//        public DateTime AppointmentDate { get; set; } // Appointment date
//        public int TokenNumber { get; set; }    // Unique token number for the appointment
//        public decimal RegistrationFee { get; set; } // Registration fee
//        public bool ConsultationStatus { get; set; } // Consultation fee
//        public decimal TotalFee { get; set; }  // Total fee calculated as RegistrationFee + ConsultationFee

//        // Helper property to validate dates (optional, can be implemented elsewhere)
//        public bool IsDateValid =>
//            AppointmentDate.Date >= DateTime.Now.Date &&
//            AppointmentDate.Date <= DateTime.Now.Date.AddDays(14);

//        // Foreign key references (optional if using relationships in EF Core or manual mapping)
//        public Patient Patient { get; set; }    // Reference to Patient class
//        public Doctor Doctor { get; set; }     // Reference to Doctor class
//    }
//}
