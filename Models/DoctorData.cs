using _17870_LP2.Interfaces;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using _17870_LP2.Commands;

namespace _17870_LP2.Models
{
    public enum Specialization
    {
        ALLERGY_IMMUNOLOGY,
        ANESTHESIOLOGY,
        DERMATOLOGY,
        DIAGNOSTIC_RADIOLOGY,
        EMERGENCY_MEDICINE,
        FAMILY_MEDICINE,
        INTERNAL_MEDICINE,
        MEDICAL_GENETICS,
        NEUROLOGY,
        NUCLEAR_MEDICINE,
        OBSTETRICS_GYNECOLOGY,
        PATHOLOGY,
        PEDIATRICS
    }
    class DoctorData : User
    {
        public int UPIN { get; set; }
        public ICollection<Specialization> Specializations { get; set; }
        public ICollection<Patient> Patients { get; set; }
        public ICollection<Hospital> Hospitals { get; set; }

        public DoctorData(string identityCard, string firstName, string lastName, string genre, int age, string contact, Address address) : base(identityCard, firstName, lastName, genre, age, contact, address)
        {
        }

    }
}
