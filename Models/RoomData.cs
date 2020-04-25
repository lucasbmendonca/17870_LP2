using _17870_LP2.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace _17870_LP2.Models
{
    class RoomData
    {
        public int Id { get; protected set; }
        public int Number { get; set; }
        public bool IsAvailable { get; set; }
        public static int MaxPatients { get; set; }
        public virtual Hospital Hospital { get; set; }
        public virtual ICollection<Patient> Patients { get; set; }
    }
}