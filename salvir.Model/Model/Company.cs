using depross.Model.Base;
using System.Collections.Generic;

namespace depross.Model
{
    /// <summary>
    /// This is the company.
    /// An account belongs to a company, which might have multiple accounts
    /// Sale listings belongs to the company, not the account.
    /// </summary>
    public class Company : BaseContactInfo
    {

        public string Name { get; set; }
        public string VAT { get; set; }

        public Image Image { get; set; }

        public virtual List<SaleListing> SaleListings { get; set; }
    }
}
