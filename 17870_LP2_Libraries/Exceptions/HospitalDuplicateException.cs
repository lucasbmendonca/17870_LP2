using System;
using System.Collections.Generic;

namespace Exceptions
{
    [Serializable]
    public class HospitalDuplicateException: Exception
    {
        public static List<Exception> _duplicateExceptions = new List<Exception>();
        public HospitalDuplicateException(string hospitalName): base(DateTime.Now.ToString() + ": Data persistence error. Hospital " + hospitalName + " is already registered.")
        {
            _duplicateExceptions.Add(this.GetBaseException());
        }
    }
}
