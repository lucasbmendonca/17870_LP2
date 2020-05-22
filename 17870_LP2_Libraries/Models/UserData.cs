using System;

namespace Models
{
    [Serializable]
    public enum Genre
    {
        M, //Male
        F //Female
    }
    /*
         Class that contains the basic properties of a user, be it a doctor or patient.
         Can be inherited.
    */
    [Serializable]
    public class UserData
    {
        public string IdentityCard { get; set; } //Unique
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Genre Genre { get; set; }
        public int Age { get; set; }
        public string Contact { get; set; }
        public Address Address { get; set; }
    }
}
