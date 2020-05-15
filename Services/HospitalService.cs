using _17870_LP2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace _17870_LP2.Services
{
    class HospitalService
    {
        #region Attributes
        private static List<Hospital> _hospitals { get; set; }
        public static int _totalCountHospitals;
        public static int _totalCountDoctors;
        public static int _totalCountPatients;
        public static int _totalCountRooms;
        #endregion

        #region Constructor
        public HospitalService()
        {
            //Initialize Hospital List from file, from database or create a new one
            _hospitals = new List<Hospital>();
        }
        #endregion

        #region Methods
        public bool CreateHospital(string hospitalName, Address hospitalAddress)
        {
            //Checks whether the internal list of hospitals is initialized
            if (_hospitals != null)
            {
                var findHospital = _hospitals.Where(i => i.Name == hospitalName).FirstOrDefault();
                //Adds new hospital if it doesn't exist in the list
                if (findHospital == null)
                {
                    var newHospital = new Hospital();
                    newHospital.Id = Interlocked.Increment(ref _totalCountHospitals);
                    newHospital.Name = hospitalName;
                    newHospital.Address = hospitalAddress;
                    newHospital.Doctors = new List<Doctor>();
                    newHospital.Patients = new List<Patient>();
                    newHospital.Rooms = new List<Room>();
                    _hospitals.Add(newHospital);
                    return true;
                }
                else
                    throw new Exception("Data persistence error. Hospital " + hospitalName + " is already registered.");
            }
            else
                throw new Exception("Internal error. The list of hospitals has not been initialized.");
        }
        public bool AddDoctor(Hospital hospital, Doctor doctor)
        {
            //Check if the hospital is already created
            if (CheckHospital(hospital))
            {
                var findDoctor = hospital.Doctors.Where(i => i.IdentityCard == doctor.IdentityCard).FirstOrDefault();
                //Adds new doctor if he doesn't exist on the hospital's doctor list
                if (findDoctor == null)
                {   
                    doctor.Id = Interlocked.Increment(ref _totalCountDoctors);
                    hospital.Doctors.Add(doctor);
                    
                    /*Adds a new hospital to the list of hospitals where the doctor works
                    doctor.Hospitals.Add(hospital);*/
                    
                    return true;
                }
                else
                    throw new Exception("Data persistence error. Doctor with Identity Card " +
                                         doctor.IdentityCard + " is already registered.");
            }
            else
                throw new Exception("Data persistence error. Hospital " + hospital.Name +
                                     " not registered.");
        }
        public bool RemoveDoctor(Hospital hospital, Doctor doctor)
        {
            //Check if the hospital is already created
            if (CheckHospital(hospital))
            {
                //Check if the doctor ID received is on the hospital's doctor list
                var findDoctor = hospital.Doctors.Where(i => i.Id == doctor.Id).FirstOrDefault();
                if (findDoctor != null)
                {
                    //Removes the doctor from the hospital's doctor list
                    hospital.Doctors.Remove(findDoctor);

                    /*Removes from the list of hospitals where the doctor works
                    doctor.Hospitals.Remove(hospital);*/
                    
                    return true;
                }
                else
                    throw new Exception("Data persistence error. Doctor does not exist in the database");
            }
            else
                throw new Exception("Data persistence error. Hospital " + hospital.Name +
                                    " not registered.");
        }

        public bool AddRoom(Hospital hospital, Room room)
        {
            //Check if the hospital is already created
            if (CheckHospital(hospital))
            {
                //Check if the room is already registered at the hospital
                var findRoom = hospital.Rooms.Where(i => i.Number == room.Number).FirstOrDefault();
                if (findRoom == null)
                {
                    room.Id = Interlocked.Increment(ref _totalCountRooms);
                    hospital.Rooms.Add(room);
                    return true;
                }
                else
                    throw new Exception("Data persistence error. Room number " + room.Number +
                                        " already exists in the database.");
            }
            else
                throw new Exception("Data persistence error. Hospital " + hospital.Name +
                                    " not registered.");
        }
        public bool RemoveRoom(Hospital hospital, Room room)
        {
            //Check if the hospital is already created
            if (!CheckHospital(hospital))
            {
                //Check if the room is already registered at the hospital
                var findRroom = hospital.Rooms.Where(i => i.Number == room.Number).FirstOrDefault();
                if (room != null)
                {
                    room.IsAvailable = false;
                    return true;
                }
                else
                    throw new Exception("Data persistence error. Room number " + room.Number +
                                        " doesn't exists in the database.");
            }
            else
                throw new Exception("Data persistence error. Hospital " + hospital.Name +
                                    " not registered.");
        }
        public bool AddPatient(Hospital hospital, Patient patient)
        {
            if (CheckHospital(hospital))
            {
                //Check if the doctor's list is associated with this hospital
                if (hospital.Doctors.All(d => patient.Doctors.Contains(d)))
                {
                    if (hospital.Rooms.Contains(patient.Room))
                    {
                        //Checks if the patient has not been admitted to any other hospital
                        if(_hospitals.ToList().Any(h => h.Patients.Contains(patient))) {
                                throw new Exception("Data persistence error. Patient with identity Card " + patient.IdentityCard +
                                                    " is already in another Hospital ");
                        }
                        //Add patient to hospital patient lists
                        patient.Id = Interlocked.Increment(ref _totalCountPatients);
                        patient.AdmissionDateTime = DateTime.Now;
                        hospital.Patients.Add(patient);
                        return true;
                    }
                    else
                        throw new Exception("Data persistence error. Room number " + patient.Room.Number +
                                            " doesn't exists in the Hospital " + hospital.Name);
                }
                else
                    throw new Exception("Data persistence error. Doctor with Identity "
                                         + patient.IdentityCard + " not registered for the Hospital " + hospital.Name);
            }
            else
                throw new Exception("Data persistence error. Hospital " + hospital.Name +
                                    " not registered.");
        }
        public bool RemovePatient(Hospital hospital, Patient patient)
        {
            if (CheckHospital(hospital))
            {
                var findPatient = hospital.Patients.Where(i => i.Id == patient.Id).FirstOrDefault();
                if (patient != null)
                {
                    findPatient.DischargeDateTime = DateTime.Now;
                    //hospital.Patients.Remove(patient);
                    return true;
                }
                else
                    throw new Exception("Data persistence error. Patient with Indetity Card " + patient.IdentityCard +
                                        " doesn't exists in the Hospital " + hospital.Name);
            }
            else
                throw new Exception("Data persistence error. Hospital " + hospital.Name +
                                    " not registered.");
        }
        private bool CheckHospital(Hospital hospital)
        {
            return _hospitals.Contains(hospital);
        }
        public Hospital GetHospital(string hospitalName)
        {
            return _hospitals.Where(h => h.Name == hospitalName).FirstOrDefault();
        }
        #endregion
    }
}