using System;

namespace Models
{
    /*
         Class containing the note data model.
         Notes are added to the patient as per medical evaluation and may contain a medical prescription.
    */
    [Serializable]
    public class Note
    {
        public string Content { get; set; }
        [NonSerialized]
        private DateTime creationDateTime;
        public string CreationDateTime {
            get { return this.creationDateTime.ToString("yyyy-MM-dd HH:mm:ss"); }
            set { this.creationDateTime = DateTime.Parse(value); }
        }
        public virtual Prescription Prescription { get; set; }
    }
}
