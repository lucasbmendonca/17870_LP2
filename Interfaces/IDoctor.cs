using _17870_LP2.Models;
using System;
using System.Collections.Generic;
using _17870_LP2.Commands;
using System.Linq;
using System.Text;

namespace _17870_LP2.Interfaces
{
    interface IDoctor
    {
        public bool AddDoctor(Doctor doctor);
        public bool RemoveDoctor(Doctor doctor);
    }
}
