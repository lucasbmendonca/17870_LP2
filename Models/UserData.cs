using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace _17870_LP2.Models
{
    class UserData
    {
        public int Id { get; protected set; }
        public string IdentityCard { get; set; } /*Unique*/
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Genre { get; set; }
        public int Age { get; set; }
        public string Contact { get; set; }
        public Address Address { get; set; }
    }
}
