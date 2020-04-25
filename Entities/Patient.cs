using _17870_LP2.Models;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using _17870_LP2.Interfaces;

namespace _17870_LP2.Commands
{
    class Patient: PatientData, IDoctor
    {
        public static int total;
        public Patient(string identityCard, string firstName, string lastName, string genre, int age, string contact, Address address): base (identityCard, firstName, lastName, genre, age, contact, address)
        {
            total++;
            AdmissionDateTime = DateTime.Today;
        }

        public Patient(string identityCard, string firstName, string lastName, string genre, int age, string contact, Address address, ICollection<Doctor> doctors): base(identityCard, firstName, lastName, genre, age, contact, address)
        {
            total++;
            AdmissionDateTime = DateTime.Today;
            AddDoctor(doctors);
        }

        public Patient(string identityCard, string firstName, string lastName, string genre, int age, string contact, Address address, Doctor doctor): base(identityCard, firstName, lastName, genre, age, contact, address)
        {
            total++;
            AdmissionDateTime = DateTime.Today;
            AddDoctor(doctor);
        }

        public bool AddDisease(Disease disease)
        {
            var containsDisease = Diseases.FirstOrDefault(disease => disease.Equals(disease));
            if (containsDisease == null)
            {
                Diseases.Add(disease);
                return true;
            }
            return false;
        }

        /*BEGIN: Interface IDoctor Methods*/
        public bool AddDoctor(Doctor doctor)
        {
            var containsDoctor = Doctors.FirstOrDefault(doctor => doctor.Equals(doctor));
            if (containsDoctor == null)
            {
                Doctors.Add(doctor);
                doctor.AddPatient(this);
                return true;
            }
            return false;
        }

        public bool RemoveDoctor(Doctor doctor)
        {
            if (doctor != null && Doctors.Contains(doctor))
            {
                Doctors.Remove(doctor);
                doctor.RemovePatient(this);
            }
            return true;
        }
        /*END: Interface IDoctor Methods*/

        public bool RemoveDoctor(ICollection<Doctor> doctors)
        {
            foreach (Doctor doctor in doctors)
            {
                RemoveDoctor(doctor);
            }
            return true;
        }

        public bool AddDoctor(ICollection<Doctor> doctors)
        {
            foreach (Doctor doctor in doctors)
            {
                AddDoctor(doctor);
            }
            return true;
        }
    }
}