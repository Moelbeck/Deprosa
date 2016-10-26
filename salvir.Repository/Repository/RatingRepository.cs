using deprosa.Repository.Abstract;
using deprosa.Repository.DatabaseContext;
using deprosa.Interfaces;
using System.Collections.Generic;
using System.Linq;
using deprosa.Model;
using System;

namespace deprosa.Repository
{
    public class RatingRepository : GenericRepository< Rating>, IRatingRepository
    {
        public RatingRepository(BzaleDatabaseContext context) : base(context)
        {

        }
        public void AddRating(Rating rating)
        {
            Add(rating);
            Save();
        }
        public Rating UpdateRating(Rating rating)
        {
            Edit(rating);
            Save();
            return GetSingle(e => e.ID == rating.ID);
        }
        public void RemoveRating(int ratingid)
        {
            Delete(ratingid);
            Save();
            
        }
        public void RemoveRating(Rating rating)
        {
            Delete(rating);
            Save();
        }
        public Rating GetRating(int ratingid)
        {
            return GetSingle(e => e.ID == ratingid);
        }
        public IQueryable<Rating> GetRatingsForCompany(Company company)
        {
            return Get(e => e.Company.ID == company.ID && e.Deleted ==null);
        }
        public IQueryable<Rating> GetRatingsForCompany(int companyid)
        {
            return Get(e => e.Company.ID == companyid && e.Deleted == null);
        }
    }
}
