using deprosa.Common;
using deprosa.Model.Base;

namespace deprosa.Model
{
    /// <summary>
    /// Images is for categories, companies, sale listing's, advertisers and advertisements
    /// </summary>
    public class Image : Entity
    {
        public string Name { get; set; }
        public string ImageURL { get; set; }
        public byte[] ImageData { get; set; }
        public eImageType Type { get; set; }
       
    }
}
