using _17870_LP2.Controllers;
using _17870_LP2.Models;
using System;

namespace _17870_LP2.View
{
    class HospitalView
    {
        #region Attributes
        public static HospitalController _hospitalController;
        #endregion

        #region Methods
        public static void SetController(HospitalController controller)
        {
            _hospitalController = controller;
        }
        public static void Display()
        {
            Console.WriteLine("Hospital's View");

            //Add new Hospital
            Address address = new Address();
            _hospitalController.CreateHospital("Adão Pereira Nunes", address);

            //Get Hospital
            Hospital hospital = _hospitalController.GetHospital("Adão Pereira Nunes");

            //Add new doctor to the hospital
            var doctor = Doctors.CreateDoctor("123", "Lucas");
            _hospitalController.AddDoctor(hospital, doctor);

            //Remove doctor from hospital
            doctor = Doctors.GetDoctor("123");
            _hospitalController.RemoveDoctor(hospital, doctor);

            //Add new patient to the hospital
            //var patient = Patients.CreatePatient(patient, doctor);
            //_hospitalController.AddPatient(hospital, patient);

            //Remove patient

            //Add room

            //Remove room

            //Save All
        }
        #endregion
    }
}
