using System;

namespace _17870_LP2.Models
{
    class Note
    {
        public string Content { get; set; }
        public DateTime CreationDateTime { get; set; }
        public virtual Prescription Prescription { get; set; }
    }
}
