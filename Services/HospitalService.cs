using _17870_LP2.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace _17870_LP2.Services
{
    class HospitalService
    {
        #region Attributes
        private static List<Hospital> _hospitals { get; set; }
        #endregion

        #region Constructor
        public HospitalService()
        {
            //Initialize Hospital List from file, from database or create a new one
            _hospitals = HospitalRepository.GetData();
        }
        #endregion

        #region Methods
        public Hospital CreateHospital(string hospitalName, Address hospitalAddress)
        {
            //Checks whether the internal list of hospitals is initialized
            if (_hospitals != null)
            {
                var findHospital = _hospitals.Where(i => i.Name == hospitalName).FirstOrDefault();
                //Adds new hospital if it doesn't exist in the list
                if (findHospital == null)
                {
                    var newHospital = new Hospital();
                    newHospital.Name = hospitalName;
                    newHospital.Address = hospitalAddress;
                    newHospital.Doctors = new List<Doctor>();
                    newHospital.Patients = new List<Patient>();
                    newHospital.Rooms = new List<Room>();
                    _hospitals.Add(newHospital);
                    return newHospital;
                }
                else
                    throw new Exception("Data persistence error. Hospital " + hospitalName + " is already registered.");
            }
            else
                throw new Exception("Internal error. The list of hospitals has not been initialized.");
        }
        public Doctor AddDoctor(Hospital hospital, Doctor doctor)
        {
            //Check if the hospital is already created
            if (CheckHospital(hospital))
            {
                var findDoctor = hospital.Doctors.Where(i => i.IdentityCard == doctor.IdentityCard).FirstOrDefault();
                //Adds new doctor if he doesn't exist on the hospital's doctor list
                if (findDoctor == null)
                {   
                    doctor.AdmissionDateTime = DateTime.Now;
                    hospital.Doctors.Add(doctor);
                    
                    /*Adds a new hospital to the list of hospitals where the doctor works
                    doctor.Hospitals.Add(hospital);*/
                    
                    return doctor;
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
                var findDoctor = hospital.Doctors.Where(i => i.IdentityCard == doctor.IdentityCard).FirstOrDefault();
                if (findDoctor != null)
                {
                    //Removes the doctor from the hospital's doctor list
                    doctor.DischargeDateTime = DateTime.Now;
                    //hospital.Doctors.Remove(findDoctor);

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
        public Room AddRoom(Hospital hospital, Room room)
        {
            //Check if the hospital is already created
            if (CheckHospital(hospital))
            {
                //Check if the room is already registered at the hospital
                var findRoom = hospital.Rooms.Where(i => i.Number == room.Number).FirstOrDefault();
                if (findRoom == null)
                {
                    hospital.Rooms.Add(room);
                    return room;
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
        public Patient AddPatient(Hospital hospital, Patient patient)
        {
            if (CheckHospital(hospital))
            {
                //Check if the doctor's list is associated with this hospital
                if (hospital.Doctors.All(d => patient.Doctors.Contains(d)))
                {
                    if (hospital.Rooms.Contains(patient.Room) && patient.Room.IsAvailable == true )
                    {
                        //Checks if the patient has not been admitted to any other hospital
                        if(_hospitals.ToList().Any(h => h.Patients.Contains(patient))) {
                                throw new Exception("Data persistence error. Patient with identity Card " + patient.IdentityCard +
                                                    " is already in another Hospital ");
                        }
                        //Add patient to hospital patient lists
                        patient.AdmissionDateTime = DateTime.Now;
                        hospital.Patients.Add(patient);
                        return patient;
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
                var findPatient = hospital.Patients.Where(i => i.IdentityCard == patient.IdentityCard).FirstOrDefault();
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
        public List<Hospital> GetAllHospitals()
        {
            return _hospitals;
        }
        public ICollection<Doctor> GetAllDoctors(string hospitalName)
        {
            return GetHospital(hospitalName).Doctors;
        }
        public ICollection<Patient> GetAllPatients(string hospitalName)
        {
            return GetHospital(hospitalName).Patients;
        }
        public ICollection<Room> GetAllRooms(string hospitalName)
        {
            return GetHospital(hospitalName).Rooms;
        }
        public bool SaveAll()
        {
            return HospitalRepository.Save(_hospitals);
        }
        #endregion
    }
}