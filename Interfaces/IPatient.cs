using _17870_LP2.Commands;
using _17870_LP2.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace _17870_LP2.Interfaces
{
    interface IPatient
    {
        public bool AddPatient(Patient patient);

        public bool RemovePatient(Patient patient);
    }
}
