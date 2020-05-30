using _17870_LP2.Controllers;
using _17870_LP2.Factory;
using _17870_LP2.Interfaces;
using Exceptions;
using Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace _17870_LP2.View
{
    /*
        Main view class for hospital management.
        In an ideal scenario, the view would have the fields 
        for the user to enter the hospital data. Input fields
        and listbox would be used, for example. In addition, 
        there would be greater control over data validation, 
        since the choice lists, in the example for the 
        doctor's specialization, would only be filled with 
        values ​​registered for that enumeration. 
        Avoiding data inconsistency.
    */
    class HospitalView : IHospitalView
    {
        #region Attributes
        /* This controller is only responsible 
           for managing the list of hospitals, 
           as well as associating patients, doctors, 
           rooms and other pertinent information 
           from one hospital.
        */
        public static HospitalController _hospitalController;
        #endregion

        #region Methods
        /// <summary>
        /// Set the main controller.
        /// </summary>

        public static void SetController(HospitalController controller)
        {
            _hospitalController = controller;
        }

        /// <summary>
        /// Display the hospital view with data filling scenarios.
        /// For the testing scenario, the information below has to be exchanged, 
        /// as it may already have been previously recorded in the BIN file.
        /// </summary>
        public static void Display()
        {
            Console.WriteLine("Hospital's View");

            #region New Hospital
            //Add new Hospital
            var hospital = CreateHospital("Adão Pereira Nunes", Addresses.CreateAddress("Rod. Washington Luiz, s/nº", "Jardim Primavera", "Duque de Caxias", "Rio de Janeiro", "Brasil", "25264-180"));
            var hospital2 = CreateHospital("Moacyr do Carmo", Addresses.CreateAddress("Rod. Washington Luíz, 3200", "Parque Beira Mar", "Duque de Caxias", "Rio de Janeiro", "Brasil", "25085-009"));
            #endregion

            #region New Doctors in Hospital
            //Add new doctor to the hospital
            List<Specialization> specializations = new List<Specialization>();
            specializations.Add(Specialization.Family_medicine);
            specializations.Add(Specialization.Neurology);
            specializations.Add(Specialization.Medical_genetics);
           
            try
            {
                //Doctor's address
                var doctorAddress = Addresses.CreateAddress("Rua A7, nº 112", "Jardim Anhangá", "Duque de Caxias", "Rio de Janeiro", "Brasil", "25264-180");
                
                var doctor = AddDoctorToHospital(hospital, Doctors.CreateDoctor("12345", "Lucas", "Braga Mendonça", Genre.M, 24, "(+55) 98078-2505", doctorAddress, specializations));

                #region New Room in Hospital
                //Add room to the hospital
                Room room = new Room();
                room.Number = 1;
                room.IsAvailable = true;
                var roomHospital = AddRoomToHospital(hospital, room);
                #endregion

                #region New Patient in Hospital
                //Add new patient to the hospital

                //address
                var patientAddress = Addresses.CreateAddress("Rua Itapeva", "Leblon", "Rio de Janeiro", "Rio de Janeiro", "Brasil", "44566-010");

                //doctors associated with patient
                List<Doctor> doctors = new List<Doctor>();
                doctors.Add(doctor);

                var patient = Patients.CreatePatient("54321", "Joãozinho", "Nunes", Genre.M, 19, "(+55)98767-5489", patientAddress, roomHospital, doctors);
                AddPatientToHospital(hospital, patient);
                #endregion

                #region Add specific informations about patient
                //Diseases
                Disease diseaseItem = new Disease();
                diseaseItem.Name = "Coronavirus";
                diseaseItem.Description = "...";
                List<Disease> diseases = new List<Disease>();
                diseases.Add(diseaseItem);

                //Notes with prescription
                Note noteItem = new Note();
                noteItem.Content = "Está com sérios problemas relacionados ao COVID.";
                noteItem.CreationDateTime = DateTime.Now.ToString();

                Prescription prescription = new Prescription();
                prescription.DaysInterval = 9;
                prescription.MedicineName = "Hidroxocloroquina";
                prescription.ValidityDate = DateTime.Now.ToString();

                noteItem.Prescription = prescription;

                patient.Diseases.Add(diseaseItem);
                patient.Notes.Add(noteItem);
                #endregion

            }
            catch (DataInconsistencyException e)
            {
                Console.WriteLine(e.Message);
            }

            try
            {
                //Doctor's address
                var doctorAddress = Addresses.CreateAddress("Rua Rodrigues, nº 112", "Imbarie", "Duque de Caxias", "Rio de Janeiro", "Brasil", "25264-180");
                
                var doctor = Doctors.CreateDoctor("54321", "Beatriz", "Braga Costa", Genre.F, 21, "(+55) 98078-2000", doctorAddress, specializations);
                AddDoctorToHospital(hospital2, doctor);
            }
            catch (DataInconsistencyException e)
            {
                Console.WriteLine(e.Message);
            }
            #endregion

            #region Remove Patients from Hospital
            /*Remove patient
            patient = Patients.GetPatient("54321");
            _hospitalController.RemovePatientFromHospital(hospital, patient);
            */
            #endregion

            #region Remove Doctors from Hospital
            /*Remove doctor from hospital
            doctor = Doctors.GetDoctor("12345");
            _hospitalController.RemoveDoctorFromHospital(hospital, doctor);
            */
            #endregion

            #region Total infected by hospital
            GetTotalInfected("Adão Pereira Nunes");
            #endregion

            #region Save Data
            //Save All
            SaveAll();
            #endregion
        }

        private static Patient AddPatientToHospital(Hospital hospital, Patient patient)
        {
            return _hospitalController.AddPatientToHospital(hospital, patient);
        }
        private static Room AddRoomToHospital(Hospital hospital, Room room)
        {
            return _hospitalController.AddRoomToHospital(hospital, room);
        }
        private static Doctor AddDoctorToHospital(Hospital hospital, Doctor doctor)
        {
            return _hospitalController.AddDoctorToHospital(hospital, doctor);
        }
        private static void SaveAll()
        {
            _hospitalController.SaveAll();
        }
        private static Hospital CreateHospital(string hospitalName, Address hospitalAddress)
        {
            return _hospitalController.CreateHospital(hospitalName, hospitalAddress);
        }
        private static void GetTotalInfected(string hospitalName)
        {
            _hospitalController.GetTotalInfected(hospitalName);
        }
        #endregion
    }
}
