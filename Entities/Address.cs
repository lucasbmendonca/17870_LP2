using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace _17870_LP2.Models
{
    class Address : AddressData
    {
        public static int totalCount;
        public Address(string address1, string address2, string city, string state, string country, string postalCode)
        {
            Id = Interlocked.Increment(ref totalCount);
            Address1 = address1;
            Address2 = address2;
            City = city;
            State = state;
            Country = country;
            PostalCode = postalCode;
        }
    }
}
