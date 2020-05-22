using System;
using System.Collections.Generic;

namespace Models
{
    /*
        List of a doctor's specializations.
    */
    [Serializable]
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

    /*
         Class containing the doctor data model.
    */
    [Serializable]
    public class Doctor : UserData
    {
        [NonSerialized]
        private DateTime admissionDateTime;
        [NonSerialized]
        private DateTime dischargeDateTime;
        public string AdmissionDateTime
        {
            get { return this.admissionDateTime.ToString("yyyy-MM-dd HH:mm:ss"); }
            set { this.admissionDateTime = DateTime.Parse(value); }
        }
        public string DischargeDateTime
        {
            get { return this.dischargeDateTime.ToString("yyyy-MM-dd HH:mm:ss"); }
            set { this.dischargeDateTime = DateTime.Parse(value); }
        }
        public ICollection<Specialization> Specializations { get; set; }
    }
}
