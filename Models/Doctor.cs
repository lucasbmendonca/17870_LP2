using System.Collections.Generic;

namespace _17870_LP2.Models
{
    public enum Specialization
    {
        Allergy_immunology,
        Anesthesiology,
        Dermatology,
        Diagnostic_radiology,
        Emergency_medicine,
        Family_medicine,
        Internal_medicine,
        Medical_genetics,
        Neurology,
        Nuclear_medicine,
        Obstertrics_gynecology,
        Pathology,
        Pediatrics
    }
    class Doctor : UserData
    {
        public ICollection<Specialization> Specializations { get; set; }
        
        //public ICollection<PatientData> Patients { get; set; }
        
        //public ICollection<HospitalData> Hospitals { get; set; }
    }
}
