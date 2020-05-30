using System;
using System.Collections.Generic;

namespace Models
{
    /*
         Class containing the patient data model.
    */
    [Serializable]
    public class Patient: UserData
    {
        [NonSerialized]
        private DateTime admissionDateTime;
        [NonSerialized]
        private DateTime dischargeDateTime;
        public string AdmissionDateTime
        {
            get { return this.admissionDateTime.ToString("yyyy/MM/dd HH:mm:ss"); }
            set { this.admissionDateTime = DateTime.Parse(value); }
        }
        public string DischargeDateTime
        {
            get { return this.dischargeDateTime.ToString("yyyy/MM/dd HH:mm:ss"); }
            set { this.dischargeDateTime = DateTime.Parse(value); }
        }
        public virtual ICollection<Doctor> Doctors { get; set; }
        public virtual ICollection<Note> Notes { get; set; }
        public virtual ICollection<Disease> Diseases { get; set; }
        public virtual Room Room { get; set; }
    }
}
