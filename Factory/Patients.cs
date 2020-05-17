using _17870_LP2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _17870_LP2.Factory
{
    class Patients
    {
        #region Attributes
        //see https://www.c-sharpcorner.com/UploadFile/c210df/difference-between-const-readonly-and-static-readonly-in-C-Sharp/
        private static readonly List<Patient> _patients;
        #endregion

        #region Constructors
        static Patients()
        {
            _patients = new List<Patient>();
        }
        #endregion

        #region Methods
        public static Patient CreatePatient(string identityCard, string firstName, string lastName, Genre genre, int age, string contact, Address address, Room room, List<Doctor> doctors)
        {
            //check if the patient already exists
            if (!(_patients.Any(d => d.IdentityCard == identityCard)))
            {
                //If not, create
                Patient patient = new Patient();
                patient.IdentityCard = identityCard;
                patient.FirstName = firstName;
                patient.LastName = lastName;
                patient.Genre = genre;
                patient.Age = age;
                patient.Contact = contact;
                patient.Address = address;
                patient.Room = room;
                patient.Doctors = doctors;
                patient.Diseases = new List<Disease>();
                patient.Notes = new List<Note>();
                
                _patients.Add(patient);
                return patient;
            }
            return null;
        }
        public static bool RemovePatient(string identityCard)
        {
            //Check if the doctor exists
            var findPatient = _patients.Where(i => i.IdentityCard == identityCard).FirstOrDefault();
            if (findPatient != null)
            {
                //If exists, remove
                _patients.Remove(findPatient);
                return true;
            }
            return false;
        }
        public static Patient GetPatient(string identityCard)
        {
            return _patients.Where(i => i.IdentityCard == identityCard).FirstOrDefault();
        }
        public static List<Patient> GetPatients()
        {
            return _patients;
        }
        #endregion
    }
}
