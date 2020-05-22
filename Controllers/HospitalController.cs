using _17870_LP2.Models;
using _17870_LP2.Services;
using _17870_LP2.View;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace _17870_LP2.Controllers
{
    /*
        The main controller class for all hospitals in the database.
        Contains all methods for performing basic hospital management.
    */
    class HospitalController : Controller
    {
        #region Attributes
        //see also https://www.c-sharpcorner.com/UploadFile/c210df/difference-between-const-readonly-and-static-readonly-in-C-Sharp/
        private static HospitalService _hospitalService; //Single instance of service class.
        #endregion

        #region Constructors
        /// <summary>
        /// Initialization of service class and associated view.
        /// </summary>
        public HospitalController()
        {
            //Create a new instance for the class of service
            //that has all the business rules.
            _hospitalService = new HospitalService();

            //Modify the controller of the main hospital view
            HospitalView.SetController(this);
            
            //Display the Hospital View
            HospitalView.Display();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Create a new hospital.
        /// </summary>
        public Hospital CreateHospital(string hospitalName, Address hospitalAddress)
        {
            try
            {
                return _hospitalService.CreateHospital(hospitalName, hospitalAddress);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return null;
        }
        
        /// <summary>
        /// Add a new doctor to one hospital.
        /// </summary>
        public Doctor AddDoctorToHospital(Hospital hospital, Doctor doctor)
        {
            try
            {
                return _hospitalService.AddDoctor(hospital, doctor);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return null;
        }
        
        /// <summary>
        /// Remove a doctor from one hospital.
        /// </summary>
        public bool RemoveDoctorFromHospital(Hospital hospital, Doctor doctor)
        {
            try
            {
                return _hospitalService.RemoveDoctor(hospital, doctor);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return false;
        }
       
        /// <summary>
        /// Add a room to one hospital.
        /// </summary>
        public Room AddRoomToHospital(Hospital hospital, Room room)
        {
            try
            {
                return _hospitalService.AddRoom(hospital, room);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return null;
        }
       
        /// <summary>
        /// Remove a room from hospital.
        /// </summary>
        public bool RemoveRoomFromHospital(Hospital hospital, Room room)
        {
            try
            {
                return _hospitalService.RemoveRoom(hospital, room);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return false;
        }
       
        /// <summary>
        /// Add a patient to one hospital.
        /// </summary>
        public Patient AddPatientToHospital(Hospital hospital, Patient patient)
        {
            try
            {
                return _hospitalService.AddPatient(hospital, patient);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return null;
        }
       
        /// <summary>
        /// Remove a patient from hospital.
        /// </summary>
        public bool RemovePatientFromHospital(Hospital hospital, Patient patient)
        {
            try
            {
                return _hospitalService.RemovePatient(hospital, patient);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return false;
        }
       
        /// <summary>
        /// Get one hospital.
        /// </summary>
        public Hospital GetHospital(string hospitalName)
        {
            return _hospitalService.GetHospital(hospitalName);
        }
       
        /// <summary>
        /// Get all hospitals.
        /// </summary>
        public List<Hospital> GetAllHospitals()
        {
            return _hospitalService.GetAllHospitals();
        }
       
        /// <summary>
        /// Get all doctors from one hospital.
        /// </summary>
        public ICollection<Doctor> GetAllDoctorsFromHospital(string hospitalName)
        {
            return _hospitalService.GetAllDoctors(hospitalName);
        }
       
        /// <summary>
        /// Get all patients from one hospital.
        /// </summary>
        public ICollection<Patient> GetAllPatientsFromHospital(string hospitalName)
        {
            return _hospitalService.GetAllPatients(hospitalName);
        }
        
        /// <summary>
        /// Get all rooms from one hospital.
        /// </summary>
        public ICollection<Room> GetAllRoomsFromHospital(string hospitalName)
        {
            return _hospitalService.GetAllRooms(hospitalName);
        }
        
        /// <summary>
        /// Save all hospital data and associated objects.
        /// </summary>
        public bool SaveAll()
        {
            return _hospitalService.SaveAll();
        }
        #endregion
    }
}