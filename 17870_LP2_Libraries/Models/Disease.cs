using System;

namespace Models
{
    /*
       Class containing the disease data model.
    */
    [Serializable]
    public class Disease
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
