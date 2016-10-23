using depross.Model.Base;
using System.Collections.Generic;

namespace depross.Model
{
    /// <summary>
    /// This is advertisers, which can have multiple advertisements
    /// </summary>
    public class Advertiser : Entity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public string URL { get; set; }

       public virtual List<Advertisement> Advertisements { get; set; } 
    }
}
