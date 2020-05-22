using _17870_LP2.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace _17870_LP2.Factory
{
    /*
        This is the class responsible for managing an address list.
    */
    class Addresses
    {
        #region Attributes
        //see https://www.c-sharpcorner.com/UploadFile/c210df/difference-between-const-readonly-and-static-readonly-in-C-Sharp/
        private static readonly List<Address> _addresses;
        #endregion

        #region Constructors
        static Addresses()
        {
            _addresses = new List<Address>();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Create a new address.
        /// </summary>
        public static Address CreateAddress(string address1, string address2, string city, string state, string country, string postalCode)
        {
            Address address = new Address();
            address.Address1 = address1;
            address.Address2 = address2;
            address.City = city;
            address.State = state;
            address.Country = country;
            address.PostalCode = postalCode;
            _addresses.Add(address);
            return address;
        }

        /// <summary>
        /// Get all addresses.
        /// </summary>
        public static List<Address> GetAllAddresses()
        {
            return _addresses;
        }
        #endregion
    }
}
