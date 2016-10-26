using deprosa.Common;
using System.ComponentModel.DataAnnotations;

namespace deprosa.ViewModel
{
    public class RatingDTO
    {
 
        public int ID { get; set; }
 
        public int CompanyID { get; set; }

        //Might needed on a highligthed rating
 
        public ImageDTO CompanyImage { get; set; }
 
        [Display(Name="Beskrivelse")]
        public string Description { get; set; }

 
        [Display(Name="Rating")]
        public eRating GivenRating { get; set; }
 
        [Display(Name="Stemmer")]
        public int Votes { get; set; }
    }
}
