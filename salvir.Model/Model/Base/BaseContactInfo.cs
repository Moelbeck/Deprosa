using depross.Common;

namespace depross.Model.Base
{
    public abstract class BaseContactInfo: Entity
    {
        public eCountryCode CountryCode { get; set; }
        public string City { get; set; }
        public int PostalCode { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}
