using deprosa.ViewModel;
using System;
using System.Collections.Generic;

namespace deprosa.Interfaces
{
    public interface IRatingWebService
    {
        bool CreateRating(RatingDTO viewmodel);

        bool RemoveRating(int viewmodel);

        bool UpdateRating(RatingDTO viewmodel);

 
        RatingDTO GetMostPositiveRatingForCompany(int companyID);

 
        List<RatingDTO> GetRatingsForCompany(int companyID, int page, int size);
    }
}
