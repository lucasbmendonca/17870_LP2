using System;
using System.Collections.Generic;

namespace Exceptions
{
    [Serializable]
    public class DataInconsistencyException: Exception
    {
        public static List<Exception> _dataExceptions = new List<Exception>();

        public DataInconsistencyException(string content): base(DateTime.Now.ToString() + ": Internal data error. " + content)
        {
            _dataExceptions.Add(this.GetBaseException());
        }
    }
}
