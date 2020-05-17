using System.Collections.Generic;

namespace _17870_LP2.Models
{
    class Hospital
    {
        public string Name { get; set; }
        public virtual Address Address { get; set; }
        public virtual ICollection<Doctor> Doctors { get; set; }
        public virtual ICollection<Patient> Patients { get; set; }
        public virtual ICollection<Room> Rooms { get; set; }
    }
}