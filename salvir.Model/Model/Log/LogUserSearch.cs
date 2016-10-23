using depross.Model.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace depross.Model.Log
{
    public class LogUserSearch: BaseLog
    {
        public string SearchString { get; set; }
    }
}
