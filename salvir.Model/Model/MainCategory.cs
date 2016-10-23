using depross.Model.Base;
using System.Collections.Generic;

namespace depross.Model
{
    /// <summary>
    /// The Main category. This might be "Cars".
    /// </summary>
    public class MainCategory : Entity
    {
        public string Name { get; set; }

        public string Description { get; set; }
        public virtual Image Image { get; set; }

    }
}
