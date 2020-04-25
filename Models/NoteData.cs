using _17870_LP2.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace _17870_LP2.Models
{
    class NoteData
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CreationDateTime { get; set; }
        public virtual Patient Patient { get; set; }
        public virtual Doctor Doctor { get; set; }
        public virtual Prescription Prescription { get; set; }
    }
}
