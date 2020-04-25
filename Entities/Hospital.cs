using _17870_LP2.Commands;
using _17870_LP2.Interfaces;
using _17870_LP2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace _17870_LP2
{
    class Hospital : HospitalData, IDoctor, IPatient
    {
        public static int totalCount;
        public Hospital(string name, Address address, Doctor director)
        {
            Id = Interlocked.Increment(ref totalCount);
            Name = name;
            Address = address;
            Director = director;
        }

        /* BEGIN: Interface Methods*/
        public bool AddDoctor(Doctor doctor)
        {
            //var containsDoctor = Doctors.Exists(doctor => doctor.Equals(doctor));
            var containsDoctor = Doctors.FirstOrDefault(doctor => doctor.Equals(doctor));
            if (containsDoctor == null)
            {
                Doctors.Add(doctor);
                doctor.AddHospital(this);
                return true;

            }
            return false;
        }

        public bool RemoveDoctor(Doctor doctor)
        {
            if (doctor != null && Doctors.Contains(doctor))
            {
                /*Removes doctor from Hospital*/
                Doctors.Remove(doctor);
                /*Removes the doctor from all associated Patients*/
                doctor.RemovePatient(doctor.Patients);
                /*Removes Hospital from Doctor*/
                doctor.RemoveHospital(this);
                return true;
            }
            return false;
        }

        public bool AddPatient(Patient patient)
        {
            var containsDoctor = false;
            /*Check if the doctor is already registered, if filled*/
            if (patient.Doctors.Count > 0)
            {
                var doctor = Doctors.FirstOrDefault(doctor => patient.Doctors.Contains(doctor));
                if (doctor != null)
                {
                    containsDoctor = true;
                }
            }
            /*If doesn't have a responsible doctor filled out yet*/
            else
            {
                containsDoctor = true;
            }

            if (containsDoctor)
            {
                var containsPatient = Patients.FirstOrDefault(patient => patient.Equals(patient));
                if (containsPatient == null)
                {
                    Patients.Add(patient);
                    return true;
                }
                return false;
            }
            else
            {
                return false; /*Unregistered associate doctor*/
            }
        }

        public bool RemovePatient(Patient patient)
        {
            if (patient != null && Patients.Contains(patient))
            {
                /*Removes from the Hospital Patients List*/
                Patients.Remove(patient);
                /*Removes the patient from all associated Doctors*/
                patient.RemoveDoctor(patient.Doctors);
                return true;
            }
            return false;
        }
        /* END: Interface Methods*/

        public bool AddDoctor(ICollection<Doctor> doctors)
        {
            foreach (Doctor doctor in doctors)
            {
                AddDoctor(doctor);
            }
            return true;
        }

        public bool RemoveDoctor(ICollection<Doctor> doctors)
        {
            foreach (Doctor doctor in doctors)
            {
                RemoveDoctor(doctor);
            }
            return true;
        }

        public bool AddPatient(ICollection<Patient> patients)
        {
            foreach (Patient patient in patients)
            {
                AddPatient(patient);
            }
            return true;
        }

        public bool RemovePatient(ICollection<Patient> patients)
        {
            foreach (Patient patient in patients)
            {
                RemovePatient(patient);
            }
            return true;
        }

        public bool AddRoom(Room room)
        {
            var containsRoom = Rooms.FirstOrDefault(room => room.Equals(room));
            if (containsRoom == null)
            {
                Rooms.Add(room);
                return true;
            }
            return false;
        }

        public bool RemoveRoom(Room room)
        {
            if (room != null && Rooms.Contains(room))
            {
                Rooms.Remove(room);
                room.RemovePatient(room.Patients);
                return true;
            }
            return false;
        }

        public override bool Equals(object obj)
        {
            return obj is HospitalData hospital &&
                   Name == hospital.Name;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name);
        }
    }
}
