namespace _17870_LP2.Models
{
    class Room
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public bool IsAvailable { get; set; }
        public int MaxPatients { get; set; }
        //public virtual ICollection<PatientData> Patients { get; set; }
    }
}