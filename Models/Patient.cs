using System;
using System.Collections.Generic;


namespace _17870_LP2.Models
{
    class Patient: UserData
    {
        public DateTime AdmissionDateTime { get; set; }
        public DateTime DischargeDateTime { get; set; }
        public virtual ICollection<Doctor> Doctors { get; set; }
        public virtual ICollection<Note> Notes { get; set; }
        public virtual ICollection<Disease> Diseases { get; set; }
        public virtual Room Room { get; set; }
    }
}
