using _17870_LP2.Models;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using _17870_LP2.Interfaces;

namespace _17870_LP2.Commands
{
    class Doctor: DoctorData, IPatient
    {
        protected static int total;
        public Doctor(string identityCard, int upin, string firstName, string lastName, string genre, int age, string contact, Address address) : base(identityCard, firstName, lastName, genre, age, contact, address)
        {
            total++;
            UPIN = upin;
        }

        public bool AddSpecialization(Specialization specialization)
        {
            var containsSpec = Specializations.Contains(specialization);
            if (!containsSpec)
            {
                Specializations.Add(specialization);
                return true;
            }
            return false;
        }

        public bool AddHospital(Hospital hospital)
        {
            var containsHospital = Hospitals.FirstOrDefault(hospital => hospital.Equals(hospital));
            if (containsHospital == null)
            {
                Hospitals.Add(hospital);
                hospital.AddDoctor(this);
                return true;
            }
            return false;
        }

        public bool RemoveHospital(Hospital hospital)
        {
            if (hospital != null && Hospitals.Contains(hospital))
            {
                Hospitals.Remove(hospital);
                hospital.RemoveDoctor(this);
                return true;
            }
            return false;
        }

        /*BEGIN: Interface IPatient Methods*/
        public bool AddPatient(Patient patient)
        {
            var containsPatient = Patients.FirstOrDefault(patient => patient.Equals(patient));
            if (containsPatient == null)
            {
                Patients.Add(patient);
                patient.AddDoctor(this);
                return true;
            }
            return false;
        }

        public bool RemovePatient(Patient patient)
        {
            if (patient != null && Patients.Contains(patient))
            {
                Patients.Remove(patient);
                patient.RemoveDoctor(this);
                return true;
            }
            return false;
        }
        /*END: Interface IPatient Methods*/

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
                Patients.Remove(patient);
            }
            return true;
        }
    }
}
