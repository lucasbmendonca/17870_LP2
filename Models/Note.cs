using System;

namespace _17870_LP2.Models
{
    class Note
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CreationDateTime { get; set; }
        public virtual Patient Patient { get; set; }
        public virtual Doctor Doctor { get; set; }
        public virtual Prescription Prescription { get; set; }
    }
}
