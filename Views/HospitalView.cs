using _17870_LP2.Controllers;
using _17870_LP2.Factory;
using _17870_LP2.Models;
using System;
using System.Collections.Generic;

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

            #region New Hospital
            //Add new Hospital
            var hospitalAddress = Addresses.CreateAddress("Rod. Washington Luiz, s/nº", "Jardim Primavera", "Duque de Caxias", "Rio de Janeiro", "Brasil", "25264-180");
            var hospital = _hospitalController.CreateHospital("Adão Pereira Nunes", hospitalAddress);

            hospitalAddress = Addresses.CreateAddress("Rod. Washington Luíz, 3200", "Parque Beira Mar", "Duque de Caxias", "Rio de Janeiro", "Brasil", "25085-009");
            var hospital2 = _hospitalController.CreateHospital("Moacyr do Carmo", hospitalAddress);
            #endregion

            #region New Doctors in Hospital
            //Add new doctor to the hospital
            List<Specialization> specializations = new List<Specialization>();
            specializations.Add(Specialization.Family_medicine);
            specializations.Add(Specialization.Neurology);
            specializations.Add(Specialization.Medical_genetics);

            //Doctor's address
            var doctorAddress = Addresses.CreateAddress("Rua A7, nº 112", "Jardim Anhangá", "Duque de Caxias", "Rio de Janeiro", "Brasil", "25264-180");
            
            var doctor = Doctors.CreateDoctor("12345", "Lucas", "Braga Mendonça", Genre.M, 24,"(+55) 98078-2505", doctorAddress, specializations);
            if (hospital != null && doctor !=null)
                _hospitalController.AddDoctorToHospital(hospital, doctor);

            var doctor2 = Doctors.CreateDoctor("54321", "Beatriz", "Braga Costa", Genre.F, 21, "(+55) 98078-2000", doctorAddress, specializations);
            if (hospital2 != null && doctor2 != null)
                _hospitalController.AddDoctorToHospital(hospital2, doctor2);
            #endregion

            #region New Room in Hospital
            //Add room to the hospital
            Room room = new Room();
            room.Number = 1;
            room.IsAvailable = true;
            var roomHospital = _hospitalController.AddRoomToHospital(hospital, room);
            #endregion

            #region New Patient in Hospital
            //Add new patient to the hospital
            
            //address
            var patientAddress = Addresses.CreateAddress("Rua Itapeva", "Leblon", "Rio de Janeiro", "Rio de Janeiro", "Brasil", "44566-010");
            
            //doctors associated with patient
            List<Doctor> doctors = new List<Doctor>();
            doctors.Add(doctor);
            
            var patient = Patients.CreatePatient("54321", "Joãozinho", "Nunes", Genre.M, 19, "(+55)98767-5489", patientAddress, roomHospital, doctors);
            _hospitalController.AddPatientToHospital(hospital, patient);
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
            noteItem.CreationDateTime = DateTime.Now;

            Prescription prescription = new Prescription();
            prescription.DaysInterval = 9;
            prescription.MedicineName = "Hidroxocloroquina";
            prescription.ValidityDate = DateTime.Now;

            noteItem.Prescription = prescription;

            patient.Diseases.Add(diseaseItem);
            patient.Notes.Add(noteItem);
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

            #region Save Data
            //Save All
            _hospitalController.SaveAll();
            #endregion
        }
        #endregion
    }
}
