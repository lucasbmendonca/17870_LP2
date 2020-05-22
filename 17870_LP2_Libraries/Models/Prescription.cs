using System;

namespace Models
{
    /*
         Class containing the prescription data model.
    */
    [Serializable]
    public class Prescription
    {
        public string MedicineName { get; set; }
        public int HoursInterval { get; set; }
        public int DaysInterval { get; set; }
        [NonSerialized]
        private DateTime validityDate;
        public string ValidityDate
        {
            get { return this.validityDate.ToString("yyyy - MM - dd HH: mm:ss"); }
            set { this.validityDate = DateTime.Parse(value); }
        }
    }
}
