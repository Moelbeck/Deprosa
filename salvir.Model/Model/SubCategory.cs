using deprosa.Model.Base;
using System.Collections.Generic;

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
