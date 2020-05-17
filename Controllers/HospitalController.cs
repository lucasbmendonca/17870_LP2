using _17870_LP2.Models;
using _17870_LP2.Services;
using _17870_LP2.View;
using System;
using System.Web.Mvc;

namespace _17870_LP2.Controllers
{
    class HospitalController : Controller
    {
        #region Attributes
        //see https://www.c-sharpcorner.com/UploadFile/c210df/difference-between-const-readonly-and-static-readonly-in-C-Sharp/
        private static HospitalService _hospitalService;
        #endregion

        #region Constructors
        public HospitalController()
        {
            _hospitalService = new HospitalService();
            HospitalView.SetController(this);
            //Display the Hospital View
            HospitalView.Display();
        }
        #endregion

        #region Actions
        /*[HttpPost]
        public ActionResult CreateHospital(string hospitalName, AddressData hospitalAddress)
        {
            try
            {
                _hospitalService.CreateHospital(hospitalName, hospitalAddress);
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return new EmptyResult();
        }*/
        #endregion

        #region Methods
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
        public Hospital GetHospital(string hospitalName)
        {
            return _hospitalService.GetHospital(hospitalName);
        }
        public bool SaveAll()
        {
            //save data into database
            return _hospitalService.SaveAll();
        }
        #endregion
    }
}