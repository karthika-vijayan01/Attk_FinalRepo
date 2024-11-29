namespace Attk_Final.Models
{
    public class AddMedicineClass
    {
        public int AppointmentId { get; set; }
            public int MedicineId { get; set; }
            public int Quantity { get; set; }
            public string Dosage { get; set; }
            public string Duration { get; set; }
            public string Frequency { get; set; }
            public bool IsMedicineStatus { get; set; }
            public DateTime CreatedDate { get; set; }
            public int CreatedBy { get; set; }
        
    }
}
