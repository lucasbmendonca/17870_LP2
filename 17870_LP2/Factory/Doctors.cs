using Models;
using System.Collections.Generic;
using System.Linq;

namespace _17870_LP2
{
    /*
        This is the class responsible for managing a doctor list.
    */
    class Doctors
    {
        #region Attributes
        //see https://www.c-sharpcorner.com/UploadFile/c210df/difference-between-const-readonly-and-static-readonly-in-C-Sharp/
        private static readonly List<Doctor> _doctors;
        #endregion

        #region Constructors
        static Doctors( )
        {
             _doctors = new List<Doctor>();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Create a new doctor.
        /// </summary>
        public static Doctor CreateDoctor(string identityCard, string firstName, string lastName, Genre genre, int age, string contact, Address address, ICollection<Specialization> specializations)
        {
            //check if the doctor already exists
            if (!(_doctors.Any(d => d.IdentityCard == identityCard)))
            {
                //If not, create
                Doctor doctor = new Doctor();
                doctor.IdentityCard = identityCard;
                doctor.FirstName = firstName;
                doctor.LastName = lastName;
                doctor.Genre = genre;
                doctor.Age = age;
                doctor.Contact = contact;
                doctor.Address = address;
                doctor.Specializations = specializations;

                _doctors.Add(doctor);
                return doctor;
            }
            return null;
        }

        /// <summary>
        /// Remove one doctor.
        /// </summary>
        public static bool RemoveDoctor(string identityCard)
        {
            //Check if the doctor exists
            var findDoctor = _doctors.Where(i => i.IdentityCard == identityCard).FirstOrDefault();
            if (findDoctor != null)
            {
                //If exists, remove
                _doctors.Remove(findDoctor);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Get one doctor by Identity Card.
        /// </summary>
        public static Doctor GetDoctor(string identityCard)
        {
            return _doctors.Where(i => i.IdentityCard == identityCard).FirstOrDefault();
        }

        /// <summary>
        /// Get all doctors.
        /// </summary>
        public static List<Doctor> GetDoctors()
        {
            return _doctors;
        }
        #endregion
    }
}
