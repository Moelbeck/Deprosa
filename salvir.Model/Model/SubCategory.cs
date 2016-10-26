using deprosa.Model.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deprosa.Model
{
    /// <summary>
    /// The Sub category. If the main is "Cars", this might be "Van"
    /// </summary>
    public class SubCategory : Entity
    {
        public string Name { get; set; }

        public string Description { get; set; }
        public virtual MainCategory MainCategory { get; set; }

        public virtual List<ProductType> Products { get; set; }
    }
}
