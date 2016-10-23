using depross.Model;
using System.Collections.Generic;
using System.Linq;

namespace depross.Interfaces
{
    public interface IRatingRepository
    {
        void AddRating(Rating rating);
        Rating UpdateRating(Rating rating);
        void RemoveRating(int ratingid);
        void RemoveRating(Rating rating);
        Rating GetRating(int ratingid);
        IQueryable<Rating> GetRatingsForCompany(Company company);
        IQueryable<Rating> GetRatingsForCompany(int companyid);
    }
}
