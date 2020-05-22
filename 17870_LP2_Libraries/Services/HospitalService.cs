using System;
using System.Collections.Generic;
using System.Linq;
using Exceptions;
using Models;
using Repository;

namespace Services
{
    /*
        The main service class for all hospitals.
        Contains all methods associated with the business rules.
    */
    public class HospitalService
    {
        #region Attributes
        //List for hospital management.
        private static List<Hospital> _hospitals { get; set; }
        #endregion

        #region Constructor
        public HospitalService()
        {
            //Initialize Hospital List from file, from database or create a new one.
            _hospitals = HospitalRepository.GetData();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Create a hospital.
        /// </summary>
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
                    throw new HospitalDuplicateException(hospitalName);
            }
            else
                throw new DataInconsistencyException("The list of hospitals has not been initialized.");
        }
        
        /// <summary>
        /// Create a doctor in hospital.
        /// </summary>
        public Doctor AddDoctor(Hospital hospital, Doctor doctor)
        {
            if (hospital == null || doctor == null) 
            {
                throw new DataInconsistencyException(System.Reflection.MethodBase.GetCurrentMethod().Name + ": Objects cannot be null."); 
            }

            //Check if the hospital is already created
            if (CheckHospital(hospital))
            {
                var findDoctor = hospital.Doctors.Where(i => i.IdentityCard == doctor.IdentityCard).FirstOrDefault();
                //Adds new doctor if he doesn't exist on the hospital's doctor list
                if (findDoctor == null)
                {   
                    doctor.AdmissionDateTime = DateTime.Now.ToString();
                    hospital.Doctors.Add(doctor);
                    
                    /*Adds a new hospital to the list of hospitals where the doctor works
                    doctor.Hospitals.Add(hospital);*/
                    
                    return doctor;
                }
                else
                    throw new DataInconsistencyException("Doctor with Identity Card " + doctor.IdentityCard + " is already registered.");
            }
            else
                throw new DataInconsistencyException("Hospital " + hospital.Name + " not registered.");
        }

        /// <summary>
        /// Remove a doctor in hospital.
        /// </summary>
        public bool RemoveDoctor(Hospital hospital, Doctor doctor)
        {
            if (hospital == null || doctor == null) 
            { 
                throw new DataInconsistencyException(System.Reflection.MethodBase.GetCurrentMethod().Name + ": Objects cannot be null."); 
            
            }
            //Check if the hospital is already created
            if (CheckHospital(hospital))
            {
                //Check if the doctor ID received is on the hospital's doctor list
                var findDoctor = hospital.Doctors.Where(i => i.IdentityCard == doctor.IdentityCard).FirstOrDefault();
                if (findDoctor != null)
                {
                    //Removes the doctor from the hospital's doctor list
                    doctor.DischargeDateTime = DateTime.Now.ToString();
                    //hospital.Doctors.Remove(findDoctor);

                    /*Removes from the list of hospitals where the doctor works
                    doctor.Hospitals.Remove(hospital);*/
                    
                    return true;
                }
                else
                    throw new DataInconsistencyException("Doctor does not exist in the database");
            }
            else
                throw new DataInconsistencyException("Hospital " + hospital.Name + " not registered.");
        }

        /// <summary>
        /// Add room in hospital.
        /// </summary>
        public Room AddRoom(Hospital hospital, Room room)
        {
             if(hospital == null || room == null) 
             { 
                throw new DataInconsistencyException(System.Reflection.MethodBase.GetCurrentMethod().Name + ": Objects cannot be null."); 
             }
                
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
                    throw new DataInconsistencyException("Room number " + room.Number + " already exists in the database.");
            }
            else
                throw new DataInconsistencyException("Hospital " + hospital.Name + " not registered.");
        }

        /// <summary>
        /// Remove room in hospital.
        /// </summary>
        public bool RemoveRoom(Hospital hospital, Room room)
        {
            if (hospital == null || room == null)
            {
                throw new DataInconsistencyException("Objects cannot be null.");
            }

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
                    throw new DataInconsistencyException("Room number " + room.Number + " doesn't exists in the database.");
            }
            else
                throw new DataInconsistencyException("Hospital " + hospital.Name + " not registered.");
        }

        /// <summary>
        /// Add patient in hospital.
        /// </summary>
        public Patient AddPatient(Hospital hospital, Patient patient)
        {
            if (hospital == null || patient == null)
            {
                throw new DataInconsistencyException(System.Reflection.MethodBase.GetCurrentMethod().Name + ": Objects cannot be null.");
            }

            if (CheckHospital(hospital))
            {
                //Check if the doctor's list is associated with this hospital
                if (hospital.Doctors.All(d => patient.Doctors.Contains(d)))
                {
                    if (hospital.Rooms.Contains(patient.Room) && patient.Room.IsAvailable == true )
                    {
                        //Checks if the patient has not been admitted to any other hospital
                        if(_hospitals.ToList().Any(h => h.Patients.Contains(patient))) {
                                throw new DataInconsistencyException("Patient with identity Card " + patient.IdentityCard +
                                                    " is already in some Hospital ");
                        }
                        //Add patient to hospital patient lists
                        patient.AdmissionDateTime = DateTime.Now.ToString();
                        hospital.Patients.Add(patient);
                        return patient;
                    }
                    else
                        throw new DataInconsistencyException("Room number " + patient.Room.Number + " doesn't exists in the Hospital " + hospital.Name);
                }
                else
                    throw new DataInconsistencyException("Doctor with Identity " + patient.IdentityCard + " not registered for the Hospital " + hospital.Name);
            }
            else
                throw new DataInconsistencyException("Hospital " + hospital.Name + " not registered.");
        }

        /// <summary>
        /// Remove patient in hospital.
        /// </summary>
        public bool RemovePatient(Hospital hospital, Patient patient)
        {
            if (hospital == null || patient == null)
            {
                throw new DataInconsistencyException(System.Reflection.MethodBase.GetCurrentMethod().Name + ": Objects cannot be null.");
            }

            if (CheckHospital(hospital))
            {
                var findPatient = hospital.Patients.Where(i => i.IdentityCard == patient.IdentityCard).FirstOrDefault();
                if (patient != null)
                {
                    findPatient.DischargeDateTime = DateTime.Now.ToString();
                    //hospital.Patients.Remove(patient);
                    return true;
                }
                else
                    throw new DataInconsistencyException("Patient with Indetity Card " + patient.IdentityCard + " doesn't exists in the Hospital " + hospital.Name);
            }
            else
                throw new DataInconsistencyException("Hospital " + hospital.Name + " not registered.");
        }

        /// <summary>
        /// Check if hospital exists in the list of hospitals.
        /// </summary>
        private bool CheckHospital(Hospital hospital)
        {
            return _hospitals.Contains(hospital);
        }

        /// <summary>
        /// Get one hospital.
        /// </summary>
        public Hospital GetHospital(string hospitalName)
        {
            return _hospitals.Where(h => h.Name == hospitalName).FirstOrDefault();
        }

        /// <summary>
        /// Get all hospitals.
        /// </summary>
        public List<Hospital> GetAllHospitals()
        {
            return _hospitals;
        }

        /// <summary>
        /// Get all doctors from one hospital.
        /// </summary>
        public ICollection<Doctor> GetAllDoctors(string hospitalName)
        {
            return GetHospital(hospitalName).Doctors;
        }

        /// <summary>
        /// Get all patients from one hospital.
        /// </summary>
        public ICollection<Patient> GetAllPatients(string hospitalName)
        {
            return GetHospital(hospitalName).Patients;
        }

        /// <summary>
        /// Get all rooms from one hospital.
        /// </summary>
        public ICollection<Room> GetAllRooms(string hospitalName)
        {
            return GetHospital(hospitalName).Rooms;
        }

        /// <summary>
        /// Save all hospitals into file/database.
        /// </summary>
        public bool SaveAll()
        {
            try
            {
                return HospitalRepository.Save(_hospitals);
            }
            catch (DataInconsistencyException e)
            {
                Console.WriteLine(e.Message);
            }
            return false;
        }
        #endregion
    }
}