using deprosa.Model.Base;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace deprosa.Model
{
    /// <summary>
    /// This is for advertisers advertisements, when we come to that part.
    /// </summary>
    public class Advertisement:Entity
    {

        public virtual Image Image { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public string URL { get; set; }


        [Column(TypeName = "DateTime2")]
        public DateTime ExpirationDate { get; set; }

        public virtual Advertiser Advertiser { get; set; }
    }
}
