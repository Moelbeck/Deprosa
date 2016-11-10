
using deprosa.Common;
using deprosa.Model.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace deprosa.Model
{
    /// <summary>
    /// This is the account of a user.
    /// </summary> 
    public class Account : BaseContactInfo    {


        public string FirstName { get; set; }
        public string LastName { get; set; }
        public eGender Gender { get; set; }
        public eAccountType Type { get; set; }
        public bool HasValidatedMail { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public int? CompanyID { get; set; }

        [ForeignKey("CompanyID")]
        public virtual Company Company { get; set; }

        public virtual List<SaleListing> Following { get; set; }

    }
}
