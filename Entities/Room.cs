using _17870_LP2.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Linq;
using _17870_LP2.Interfaces;

namespace _17870_LP2.Commands
{
    class Room: RoomData, IPatient
    {
        public static int totalCount;
        public Room(int number, bool isAvailable, int maxPatients, Hospital hospital)
        {
            Id = Interlocked.Increment(ref totalCount);
            Number = number;
            MaxPatients = maxPatients;
            IsAvailable = isAvailable;
            Hospital = hospital;
        }

        public override bool Equals(object obj)
        {
            return obj is Room room &&
                   Number == room.Number &&
                   EqualityComparer<Hospital>.Default.Equals(Hospital, room.Hospital);
        }

        public bool AddPatient(Patient patient)
        {
            var containsPatient = Patients.FirstOrDefault(patient => patient.Equals(patient));
            if (containsPatient == null)
            {
                Patients.Add(patient);
                patient.Room = this;
                return true;
            }
            return false;
        }

        public bool AddPatient(ICollection<Patient> patients)
        {
            foreach (Patient patient in patients)
            {
                AddPatient(patient);
            }
            return true;
        }

        public bool RemovePatient(Patient patient)
        {
            if (patient != null && Patients.Contains(patient))
            {
                Patients.Remove(patient);
                patient.Room = null;
                return true;
            }
            return false;
        }

        public bool RemovePatient(ICollection<Patient> patients)
        {
            foreach (Patient patient in patients)
            {
                Patients.Remove(patient);
            }
            return true;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Number, Hospital);
        }
    }
}
