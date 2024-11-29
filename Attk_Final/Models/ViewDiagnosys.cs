using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Attk_Final.Models
{
    public class ViewDiagnosys
    {
            public int MainPrescriptionId { get; set; }   
            
            public string Symptoms { get; set; }
        public string Diagnosis { get; set; }
        public string TreatmentPlan { get; set; } 
            public int AppointmentId { get; set; } 
            public DateTime CreatedDate { get; set; }
            public int CreatedBy { get; set; }    
    }
}
