using Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FileUtilities
{
    /*
        Class of utilities for manipulating CSV files for hospital data.
    */
    public class HospitalUtilCSV
    {
        #region Attributes
        private static string basePath;
        private static string hospitalPath;
        private static string doctorPath;
        private static string patientPath;
        private static string roomPath;
        public static List<Hospital> _hospitals;
        #endregion

        #region Constructor
        static HospitalUtilCSV()
        {
            _hospitals = new List<Hospital>();
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
            hospitalPath = Path.Combine(basePath, "Hospitals" + ".csv");
            doctorPath = Path.Combine(basePath, "Doctors" + ".csv");
            patientPath = Path.Combine(basePath, "Patients" + ".csv");
            roomPath = Path.Combine(basePath, "Rooms" + ".csv");
        }

        /// <summary>
        /// Read doctors from CSV file.
        /// </summary>
        public static void DoctorReader()
        {
            string line = "";
            string[] lineColumn = null;
            try {
                StreamReader reader = new StreamReader(doctorPath, Encoding.UTF8, true);
                line = reader.ReadLine();
                while (true)
                {
                    line = reader.ReadLine();
                    if (line == null) break;
                    lineColumn = line.Split(';');

                    Doctor doctorItem = new Doctor();
                    doctorItem.AdmissionDateTime = lineColumn[1];
                    doctorItem.DischargeDateTime = lineColumn[2];
                    doctorItem.Specializations = new List<Specialization>();
                    var specializations = lineColumn[3].Split(",");
                    foreach (var specialization in specializations)
                    {
                        Specialization s;
                        Enum.TryParse(specialization, out s);
                        doctorItem.Specializations.Add(s);
                    }
                    doctorItem.IdentityCard = lineColumn[4];
                    doctorItem.FirstName = lineColumn[5];
                    doctorItem.LastName = lineColumn[6];
                    Genre g;
                    Enum.TryParse(lineColumn[7], out g);
                    doctorItem.Genre = g;
                    doctorItem.Age = Int32.Parse(lineColumn[8]);
                    doctorItem.Address = new Address();
                    doctorItem.Contact = lineColumn[9];
                    doctorItem.Address.Address1 = lineColumn[10];
                    doctorItem.Address.Address2 = lineColumn[11];
                    doctorItem.Address.City = lineColumn[12];
                    doctorItem.Address.State = lineColumn[13];
                    doctorItem.Address.Country = lineColumn[14];
                    doctorItem.Address.PostalCode = lineColumn[15];

                    var hospital = _hospitals.FirstOrDefault(h => h.Name == lineColumn[0]);
                    if (hospital != null)
                    {
                        if (hospital.Doctors == null)
                        {
                            hospital.Doctors = new List<Doctor>();
                        }
                        hospital.Doctors.Add(doctorItem);
                    }
                }
                reader.Close();
            } catch (FileNotFoundException e)
            {
                Console.WriteLine(e.Message);
            }   
        }

        /// <summary>
        /// Read hospitals from CSV file.
        /// </summary>
        public static void HospitalReader()
        {
            string line = "";
            string[] lineColumn = null;
            try
            {
                StreamReader reader = new StreamReader(hospitalPath, Encoding.UTF8, true);
                line = reader.ReadLine();
                while (true)
                {
                    line = reader.ReadLine();
                    if (line == null) break;
                    lineColumn = line.Split(';');

                    Hospital hospitalItem = new Hospital();
                    hospitalItem.Name = lineColumn[0];
                    hospitalItem.Address = new Address();
                    hospitalItem.Address.Address1 = lineColumn[1];
                    hospitalItem.Address.Address2 = lineColumn[2];
                    hospitalItem.Address.City = lineColumn[3];
                    hospitalItem.Address.State = lineColumn[4];
                    hospitalItem.Address.Country = lineColumn[5];
                    hospitalItem.Address.PostalCode = lineColumn[6];
                    hospitalItem.Rooms = new List<Room>();
                    _hospitals.Add(hospitalItem);
                }
                reader.Close();
            } 
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e.Message);

            }
        }

        /// <summary>
        /// Read rooms from CSV file.
        /// </summary>
        public static void RoomReader()
        {
            string line = "";
            string[] lineColumn = null;
            try
            {
                StreamReader reader = new StreamReader(roomPath, Encoding.UTF8, true);
                line = reader.ReadLine();
                while (true)
                {
                    line = reader.ReadLine();
                    if (line == null) break;
                    lineColumn = line.Split(';');

                    Room roomItem = new Room();
                    roomItem.Number = Int32.Parse(lineColumn[1]);
                    roomItem.IsAvailable = Boolean.Parse(lineColumn[2]);

                    var hospital = _hospitals.FirstOrDefault(h => h.Name == lineColumn[0]);
                    if (hospital != null)
                    {
                        hospital.Rooms.Add(roomItem);
                    }
                }
                reader.Close();
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Read patients from CSV file.
        /// </summary>
        public static void PatientReader()
        {
            string line = "";
            string[] lineColumn = null;
            try {
                StreamReader reader = new StreamReader(patientPath, Encoding.UTF8, true);
                line = reader.ReadLine();
                while (true)
                {
                    line = reader.ReadLine();
                    if (line == null) break;
                    lineColumn = line.Split(';');

                    Patient patientItem = new Patient();
                    patientItem.AdmissionDateTime = lineColumn[1];
                    patientItem.DischargeDateTime = lineColumn[2];
                    patientItem.Doctors = new List<Doctor>();
                    var doctors = lineColumn[3].Split(",");
                    foreach (var doctor in doctors)
                    {
                        //search hospital
                        var h = _hospitals.Where(h => h.Name == lineColumn[0]).FirstOrDefault();
                        if (h != null)
                        {
                            //search doctor
                            var d = h.Doctors.Where(d => d.IdentityCard == doctor).FirstOrDefault();
                            if (d != null)
                            {
                                patientItem.Doctors.Add(d);
                            }
                        }
                    }
                    patientItem.Diseases = new List<Disease>();
                    var diseases = lineColumn[4].Split(",");
                    foreach (var disease in diseases)
                    {
                        if(disease != "") 
                        {
                            Disease d = new Disease();
                            d.Name = disease;
                            patientItem.Diseases.Add(d);
                        }
                    }

                    patientItem.IdentityCard = lineColumn[6];
                    patientItem.FirstName = lineColumn[7];
                    patientItem.LastName = lineColumn[8];
                    Genre g;
                    Enum.TryParse(lineColumn[9], out g);
                    patientItem.Genre = g;
                    patientItem.Age = Int32.Parse(lineColumn[10]);
                    patientItem.Address = new Address();
                    patientItem.Contact = lineColumn[11];
                    patientItem.Address.Address1 = lineColumn[12];
                    patientItem.Address.Address2 = lineColumn[13];
                    patientItem.Address.City = lineColumn[14];
                    patientItem.Address.State = lineColumn[15];
                    patientItem.Address.Country = lineColumn[16];
                    patientItem.Address.PostalCode = lineColumn[17];

                    var hospital = _hospitals.FirstOrDefault(h => h.Name == lineColumn[0]);
                    if (hospital != null)
                    {
                        if (hospital.Patients == null)
                        {
                            hospital.Patients = new List<Patient>();
                        }
                        hospital.Patients.Add(patientItem);
                    }

                    var room = hospital.Rooms.Where(r => r.Number == Int32.Parse(lineColumn[5])).FirstOrDefault();
                    if(room != null) 
                    { 
                        patientItem.Room = room; 
                    }
                    
                }
                reader.Close();
            } catch (FileNotFoundException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Create hospitals CSV file.
        /// </summary>
        public static void CreateHospitalCSV()
        {
            var sb = new StringBuilder();
            var header = "";
            try
            {
                var file = File.Create(hospitalPath);
                file.Close();
                foreach (var prop in typeof(Hospital).GetProperties())
                {
                    if (prop.Name == "Address")
                    {
                        foreach (var propaux in typeof(Address).GetProperties())
                        {
                            header += propaux.Name + "; ";
                        }
                        continue;
                    }
                    if (prop.Name == "Name")
                    {
                        header += prop.Name + "; ";
                    }
                }
                header = header.Substring(0, header.Length - 2);
                sb.AppendLine(header);
                TextWriter sw = new StreamWriter(hospitalPath, false, Encoding.UTF8);
                sw.Write(sb.ToString());
                sw.Close();
            } catch (IOException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Create doctors CSV file.
        /// </summary>
        public static void CreateDoctorCSV()
        {
            var sb = new StringBuilder();
            var header = "Hospital; "; //Column for Hospital's name
            try
            {
                var file = File.Create(doctorPath);
                file.Close();
                foreach (var prop in typeof(Doctor).GetProperties())
                {
                    if (prop.Name == "Address")
                    {
                        foreach (var propaux in typeof(Address).GetProperties())
                        {
                            header += propaux.Name + "; ";
                        }
                        continue;
                    }
                    header += prop.Name + "; ";
                }
                header = header.Substring(0, header.Length - 2);
                sb.AppendLine(header);
                TextWriter sw = new StreamWriter(doctorPath, false, Encoding.UTF8);
                sw.Write(sb.ToString());
                sw.Close();
            } catch(IOException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Create patients CSV file.
        /// </summary>
        public static void CreatePatientCSV()
        {
            var sb = new StringBuilder();
            var header = "Hospital; "; //Column for Hospital's name
            try
            {
                var file = File.Create(patientPath);
                file.Close();
                foreach (var prop in typeof(Patient).GetProperties())
                {
                    if (prop.Name == "Address")
                    {
                        foreach (var propaux in typeof(Address).GetProperties())
                        {
                            header += propaux.Name + "; ";
                        }
                        continue;
                    }
                    if (prop.Name == "Notes")
                    {
                        continue;
                    }
                    header += prop.Name + "; ";
                }
                header = header.Substring(0, header.Length - 2);
                sb.AppendLine(header);
                TextWriter sw = new StreamWriter(patientPath, false, Encoding.UTF8);
                sw.Write(sb.ToString());
                sw.Close();
            } catch(IOException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Create rooms from CSV file.
        /// </summary>
        public static void CreateRoomCSV()
        {
            var sb = new StringBuilder();
            var header = "Hospital; "; //Column for Hospital's name
            try
            {
                var file = File.Create(roomPath);
                file.Close();
                foreach (var prop in typeof(Room).GetProperties())
                {
                    header += prop.Name + "; ";
                }
                header = header.Substring(0, header.Length - 2);
                sb.AppendLine(header);
                TextWriter sw = new StreamWriter(roomPath, false, Encoding.UTF8);
                sw.Write(sb.ToString());
                sw.Close();
            } catch (IOException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Write doctors in the CSV file.
        /// </summary>
        public static void WriteDoctorToCSV(Hospital hospital, ICollection<Doctor> doctors)
        {
            if(doctors == null) { return; }
            foreach (var obj in doctors)
            {
                StringBuilder sb = new StringBuilder();
                var line = hospital.Name + "; "; //Doctor's Hospital Name
                foreach (var prop in typeof(Doctor).GetProperties())
                {
                    if (prop.Name == "Address")
                    {
                        foreach (var propaux in typeof(Address).GetProperties())
                        {
                            line += propaux.GetValue(obj.Address, null) + "; ";
                        }
                        continue;
                    }
                    if (prop.Name == "Specializations")
                    {
                        foreach (var specialization in obj.Specializations)
                        {
                            line += specialization.ToString() + ", ";
                        }
                        line += "; ";
                        continue;
                    }
                    line += prop.GetValue(obj, null) + "; ";
                }
                line = line.Substring(0, line.Length - 2);
                sb.AppendLine(line);
                try
                {
                    TextWriter sw = new StreamWriter(doctorPath, true);
                    sw.Write(sb.ToString());
                    sw.Close();
                } catch(IOException e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        /// <summary>
        /// Write hospitals in the CSV file.
        /// </summary>
        public static void WriteHospitalToCSV(Hospital obj)
        {
            if(obj == null) { return; }
            StringBuilder sb = new StringBuilder();
            var line = "";
            foreach (var prop in typeof(Hospital).GetProperties())
            {
                if (prop.Name == "Address")
                {
                    foreach (var propaux in typeof(Address).GetProperties())
                    {
                        line += propaux.GetValue(obj.Address, null) + "; ";
                    }
                    continue;
                }
                if (prop.Name == "Name")
                {
                    line += prop.GetValue(obj, null) + "; ";
                }
            }
            line = line.Substring(0, line.Length - 2);
            sb.AppendLine(line);
            try
            {
                TextWriter sw = new StreamWriter(hospitalPath, true);
                sw.Write(sb.ToString());
                sw.Close();
            } catch (IOException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Write patients in the CSV file.
        /// </summary>
        public static void WritePatientToCSV(Hospital hospital, ICollection<Patient> patients)
        {
            if(patients == null) { return; }
            foreach (var obj in patients)
            {
                StringBuilder sb = new StringBuilder();
                var line = hospital.Name + "; "; //Doctor's Hospital Name
                foreach (var prop in typeof(Patient).GetProperties())
                {
                    if (prop.Name == "Address")
                    {
                        foreach (var propaux in typeof(Address).GetProperties())
                        {
                            line += propaux.GetValue(obj.Address, null) + "; ";
                        }
                        continue;
                    }
                    if (prop.Name == "Doctors")
                    {
                        foreach (var doctor in obj.Doctors)
                        {
                            line += doctor.IdentityCard + ", ";
                        }
                        line += "; ";
                        continue;
                    }
                    if (prop.Name == "Diseases")
                    {
                        foreach (var disease in obj.Diseases)
                        {
                            line += disease.Name.ToString() + ", ";
                        }
                        line += "; ";
                        continue;
                    }
                    if (prop.Name == "Room")
                    {
                        line += obj.Room.Number.ToString() + "; ";
                        continue;
                    }
                    if (prop.Name == "Notes") { continue; }

                    line += prop.GetValue(obj, null).ToString() + "; ";
                }
                line = line.Substring(0, line.Length - 2);
                sb.AppendLine(line);
                
                try 
                { 
                    TextWriter sw = new StreamWriter(patientPath, true);
                    sw.Write(sb.ToString());
                    sw.Close();
                } catch (IOException e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        /// <summary>
        /// Write rooms in the CSV file.
        /// </summary>
        public static void WriteRoomToCSV(Hospital hospital, ICollection<Room> rooms)
        {
            if(rooms == null) { return; }
            
            foreach (var obj in rooms)
            {
                StringBuilder sb = new StringBuilder();
                var line = hospital.Name + "; "; //Doctor's Hospital Name
                foreach (var prop in typeof(Room).GetProperties())
                {
                    line += prop.GetValue(obj, null).ToString() + "; ";
                }
                line = line.Substring(0, line.Length - 2);
                sb.AppendLine(line);
                try
                {
                    TextWriter sw = new StreamWriter(roomPath, true);
                    sw.Write(sb.ToString());
                    sw.Close();
                } catch (IOException e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
        #endregion
    }
}
