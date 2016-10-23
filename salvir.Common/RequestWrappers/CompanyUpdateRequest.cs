using depross.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace depross.Common
{
    public class CompanyUpdateRequest
    {
        public int AccountID { get; set; }
        public CompanyDTO Company { get; set; }
    }
}
