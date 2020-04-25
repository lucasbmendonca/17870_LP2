using _17870_LP2.Commands;
using _17870_LP2.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace _17870_LP2.Models
{
    class HospitalData
    {
        public int Id { get; protected set; }
        public string Name { get; set; }
        public virtual Address Address { get; set; }
        public virtual Doctor Director { get; set; }
        public virtual ICollection<Doctor> Doctors { get; set; }
        public virtual ICollection<Patient> Patients { get; set; }
        public virtual ICollection<Room> Rooms { get; set; }
    }
}