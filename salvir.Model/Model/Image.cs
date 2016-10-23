using depross.Common;
using depross.Model.Base;

namespace depross.Model
{
    /// <summary>
    /// Images is for categories, companies, sale listing's, advertisers and advertisements
    /// </summary>
    public class Image : Entity
    {

        public string ImageURL { get; set; }

        public byte[] ImageData { get; set; }

        public eImageType Type { get; set; }
       
    }
}
