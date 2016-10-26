using deprosa.Common;
using deprosa.Model.Base;

namespace deprosa.Model
{
    /// <summary>
    /// This is product types a manufacturer have.
    /// A product type refers to a sub category.
    /// This might vary from product type, as a manufacturer can deliver different product types.
    /// </summary>
    public class ProductType : Entity
    {
        public string Name { get; set; }

        //public virtual Manufacturer Manufacturer { get; set; }
        public virtual SubCategory Category { get; set; }

        //Tells which properties this type will use on the sale listing.
        public eSalelistingTypes Types { get; set; }

    }
}
