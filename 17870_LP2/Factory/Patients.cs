using Models;
using System.Collections.Generic;
using System.Linq;

namespace _17870_LP2.Factory
{
    /*
        This is the class responsible for managing a patient list.
    */
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
        /// <summary>
        /// Create a new patient.
        /// </summary>
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
        
        /// <summary>
        /// Remove one patient.
        /// </summary>
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

        /// <summary>
        /// Get one patient by Identity Card.
        /// </summary>
        public static Patient GetPatient(string identityCard)
        {
            return _patients.Where(i => i.IdentityCard == identityCard).FirstOrDefault();
        }

        /// <summary>
        /// Add one note to a patient.
        /// </summary>
        public static Patient AddNote(string identityCard, Note note)
        {
            if (note == null) { return null; }
            var patient = GetPatient(identityCard);
            if (patient != null)
            {
                patient.Notes.Add(note);
            }
            return patient;
        }

        /// <summary>
        /// Add a doctor to patient.
        /// </summary>
        public static Patient AddDoctor(string identityCard, Doctor doctor)
        {
            if (doctor == null && identityCard == null) { return null; }
            var patient = GetPatient(identityCard);
            if (patient != null)
            {
                patient.Doctors.Add(doctor);
            }
            return patient;
        }

        /// <summary>
        /// Remove one doctor from patient.
        /// </summary>
        public static Patient RemoveDoctor(string identityCard, Doctor doctor)
        {
            if (doctor == null && identityCard == null) { return null; }
            var patient = GetPatient(identityCard);
            if (patient != null)
            {
                if (patient.Doctors.Contains(doctor)){
                    patient.Doctors.Remove(doctor);
                }
            }
            return patient;
        }

        /// <summary>
        /// Get all patients.
        /// </summary>
        public static List<Patient> GetPatients()
        {
            return _patients;
        }
        #endregion
    }
}
