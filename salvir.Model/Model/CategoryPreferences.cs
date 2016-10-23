using depross.Model.Base;
using System.Collections.Generic;

namespace depross.Model
{
    /// <summary>
    /// This is a user's category preferences (Recommendation system needs to be implemented)
    /// </summary>
    public class CategoryPreferences : Entity
    {
        public virtual Account Account { get; set; }

        public virtual List<MainCategory> PreferedCategories { get; set; }       
    }
}
