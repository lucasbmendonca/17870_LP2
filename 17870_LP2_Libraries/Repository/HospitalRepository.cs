using Exceptions;
using FileUtilities;
using Models;
using System;
using System.Collections.Generic;

namespace Repository
{
    /*
         Class responsible for manipulating the database.
    */
    public class HospitalRepository
    {
        #region Methods

        /// <summary>
        /// Save all hospitals into database/file.
        /// </summary>
        public static bool Save(List<Hospital> hospitals)
        {
            if (hospitals == null) 
            {
                throw new DataInconsistencyException("Objects cannot be null.");
            }
            //return SaveToCSV(hospitals); //Save CSV file.
            return SaveToBIN(hospitals); //Save BIN file.
        }

        /// <summary>
        /// Get data from database/file.
        /// </summary>
        public static List<Hospital> GetData()
        {
            //return GetDataFromCSV();
            return GetDataFromBIN();
        }

        /// <summary>
        /// Save hospitals to a BIN file.
        /// </summary>
        private static bool SaveToBIN(List<Hospital> hospitals)
        {
            try
            {
                return HospitalUtilBIN.WriteToBIN(hospitals);
            }
            catch (DataInconsistencyException e)
            {
                Console.WriteLine(e.ToString() + ": " + e.Message);
            }
            return false;
        }

        /// <summary>
        /// Get data from a BIN file.
        /// </summary>
        private static List<Hospital> GetDataFromBIN()
        {
            try
            {
                return HospitalUtilBIN.ReadFromBIN();
            }
            catch (DataInconsistencyException e)
            {
                Console.WriteLine(e.Message);
            }
            return null;
        }

        /// <summary>
        /// Save data into CSV file.
        /// </summary>
        private static bool SaveToCSV(List<Hospital> hospitals)
        {
            HospitalUtilCSV.CreateHospitalCSV();    //Create Hospitals.csv
            HospitalUtilCSV.CreateDoctorCSV();      //Create Doctor.csv
            HospitalUtilCSV.CreatePatientCSV();     //Create Patient.csv
            HospitalUtilCSV.CreateRoomCSV();        //Create Room.csv

            foreach (var obj in hospitals)
            {
                HospitalUtilCSV.WriteHospitalToCSV(obj);
                HospitalUtilCSV.WriteDoctorToCSV(obj, obj.Doctors);
                HospitalUtilCSV.WritePatientToCSV(obj, obj.Patients);
                HospitalUtilCSV.WriteRoomToCSV(obj, obj.Rooms);
            }
            return true;
        }

        /// <summary>
        /// Get data from a CSV file.
        /// </summary>
        private static List<Hospital> GetDataFromCSV()
        {
            HospitalUtilCSV.HospitalReader();
            HospitalUtilCSV.DoctorReader();
            HospitalUtilCSV.RoomReader();
            HospitalUtilCSV.PatientReader();
            return HospitalUtilCSV._hospitals;
        }
        #endregion
    }
}
