using System;
using System.ComponentModel.DataAnnotations;

namespace Attk_Final.Models
{
    public class ViewBook
    {
        public int AppointmentId { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public int TokenNumber { get; set; }
        public bool ConsultationStatus { get; set; }
    }

}

