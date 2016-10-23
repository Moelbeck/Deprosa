using depross.Model.Base;
using System.Collections.Generic;

namespace depross.Model
{
    /// <summary>
    /// Manufactures of product types.
    /// </summary>
    public class Manufacturer : Entity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        //List of product tyåes this manufacturer produces 
        public virtual List<ProductType> ProductTypes { get; set; }

    }
}
