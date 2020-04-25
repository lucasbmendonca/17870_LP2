using _17870_LP2.Models;
using Microsoft.OData.Edm;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace _17870_LP2.Commands
{
    class Prescription: PrescriptionData
    {
        public static int totalCount;
        public Prescription(string medicineName, int hoursInterval, int daysInterval, Date validityDate)
        {
            Id = Interlocked.Increment(ref totalCount);
            MedicineName = medicineName;
            HoursInterval = hoursInterval;
            DaysInterval = daysInterval;
            ValidityDate = validityDate;
        }
    }
}
