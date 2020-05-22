using Exceptions;
using Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace FileUtilities
{
    /*
        Class of utilities for manipulating binary files for hospital data.
    */
    public class HospitalUtilBIN
    {
        #region Attributes
        private static string basePath;
        private static string hospitalPath;
        private static string exceptionPath;
        private const string c_hospitalbin = "Hospitals.BIN";
        private const string c_exceptionbin = "Exceptions.BIN";
        #endregion

        #region Constructor
        static HospitalUtilBIN()
        {
            PathsInitialize();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Mount path for file manipulation.
        /// </summary>
        private static void PathsInitialize()
        {
            basePath = Environment.CurrentDirectory;
            hospitalPath = Path.Combine(basePath, c_hospitalbin);
            exceptionPath = Path.Combine(basePath, c_exceptionbin);
        }

        /// <summary>
        /// Writes data to binary file.
        /// </summary>
        public static bool WriteToBIN(List<Hospital> hospitals)
        {
            try
            {
                //Create the stream to add object into it.
                Stream ms = File.OpenWrite(hospitalPath);
                try
                {
                    //Format the object as Binary
                    BinaryFormatter formatter = new BinaryFormatter();
                    //It serialize the list of hospitals object
                    formatter.Serialize(ms, hospitals);
                    ms.Flush();
                    ms.Close();
                    ms.Dispose();

                    //It serialize the exception messages
                    ms = File.OpenWrite(exceptionPath);
                    formatter.Serialize(ms, HospitalDuplicateException._duplicateExceptions);
                    formatter.Serialize(ms, DataInconsistencyException._dataExceptions);
                    ms.Flush();
                    ms.Close();
                    ms.Dispose();
                    return true;
                }
                catch (SerializationException e)
                {
                    ms.Close();
                    throw new DataInconsistencyException(e.Message + ". It was not possible to save the objects.");
                }
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message);
            }
            return false;
        }

        /// <summary>
        /// Read data from binary file.
        /// </summary>
        public static List<Hospital> ReadFromBIN()
        {
            List<Hospital> hospitals = new List<Hospital>();
            try
            {
                //Format the object as Binary
                BinaryFormatter formatter = new BinaryFormatter();

                //Reading the file from the server
                FileStream fs = File.Open(hospitalPath, FileMode.Open);
                try
                {
                    object obj = formatter.Deserialize(fs);
                    hospitals = (List<Hospital>)obj;
                    fs.Flush();
                    fs.Close();
                    fs.Dispose();
                }
                catch (SerializationException e)
                {
                    fs.Close();
                    throw new DataInconsistencyException(e.Message + ". It was not possible to save the objects.");
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("Data file not yet available");
            }
            return hospitals;
        }
        #endregion
    }
}