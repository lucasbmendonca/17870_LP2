using Microsoft.OData.Edm;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace _17870_LP2.Models
{
    class Prescription
    {
        public string MedicineName { get; set; }
        public int HoursInterval { get; set; }
        public int DaysInterval { get; set; }
        public Date ValidityDate { get; set; }
    }
}
