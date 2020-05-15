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
        public static Doctor CreateDoctor(string identityCard, string firstName)
        {  
            if (!(_doctors.Any(d => d.IdentityCard == identityCard)))
            {
                Doctor doctor = new Doctor();
                doctor.IdentityCard = identityCard;
                doctor.FirstName = firstName;
                _doctors.Add(doctor);
                return doctor;
            }
            return null;
        }
        public static bool RemoveDoctor(Doctor doctor)
        {
            var findDoctor = _doctors.Where(i => i.IdentityCard == doctor.IdentityCard).FirstOrDefault();
            if (findDoctor != null)
            {
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
