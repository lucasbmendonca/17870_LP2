using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace _17870_LP2.Models
{
    public enum Genre
    {
        M, //Male
        F //Female
    }
    class UserData
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
