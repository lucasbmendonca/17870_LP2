using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace _17870_LP2.Models
{
    class Disease : DiseaseData
    {
        public Disease(string name, string description)
        {
            Id = Interlocked.Increment(ref totalCount);
            Name = name;
            Description = description;
        }
        public override bool Equals(object obj)
        {
            return obj is Disease disease &&
                   Name == disease.Name;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(Name);
        }
    }
}
