using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using deprosa.Model.Base;

namespace deprosa.Model.Log
{
    public class LogCategory : BaseLog
    {
        public int MainCateogryId { get; set; }
        public int SubCategoryId { get; set; }  
    }
}
