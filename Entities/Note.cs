using _17870_LP2.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace _17870_LP2.Commands
{
    class Note: NoteData
    {
        protected static int totalCount;
        public Note(string content, DateTime creationDateTime, Patient patient, Doctor doctor, Prescription prescription)
        {
            Id = Interlocked.Increment(ref totalCount);
            Content = content;
            CreationDateTime = creationDateTime;
            Patient = patient;
            Doctor = doctor;
            Prescription = prescription;
        }
    }
}
