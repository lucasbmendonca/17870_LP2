using System;
using System.Collections.Generic;

namespace Models
{
    /*
         Class containing the hospital data model.
    */
    [Serializable]
     public class Hospital
     {
        public string Name { get; set; }
        public virtual Address Address { get; set; }
        public virtual ICollection<Doctor> Doctors { get; set; }
        public virtual ICollection<Patient> Patients { get; set; }
        public virtual ICollection<Room> Rooms { get; set; }
    }
}