using _17870_LP2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _17870_LP2
{
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
        public static Doctor GetDoctor(string identityCard)
        {
            return _doctors.Where(i => i.IdentityCard == identityCard).FirstOrDefault();
        }
        public static List<Doctor> GetDoctors()
        {
            return _doctors;
        }
        #endregion
    }
}
