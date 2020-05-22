using System;

namespace Models
{
    /*
         Class containing the room data model.
         The room belongs to one patient or more.
    */
    [Serializable]
    public class Room
    {
        public int Number { get; set; }
        public bool IsAvailable { get; set; }
    }
}