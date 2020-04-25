using _17870_LP2.Commands;
using _17870_LP2.Interfaces;
using Microsoft.OData.Edm;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _17870_LP2.Models
{
    class PatientData: User
    {
        public PatientData(string identityCard, string firstName, string lastName, string genre, int age, string contact, Address address) : base(identityCard, firstName, lastName, genre, age, contact, address)
        {
        }
        public DateTime AdmissionDateTime { get; set; }
        public DateTime DischargeDateTime { get; set; }
        public virtual ICollection<Doctor> Doctors { get; set; }
        public virtual ICollection<Note> Notes { get; set; }
        public virtual ICollection<Disease> Diseases { get; set; }
        public virtual Room Room { get; set; }
    }
}
