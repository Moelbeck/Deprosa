using AutoMapper;
using depross.Interfaces;
using depross.Model;
using depross.Repository;
using depross.Repository.DatabaseContext;
using depross.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace depross.WebService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "RatingService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select RatingService.svc or RatingService.svc.cs at the Solution Explorer and start debugging.
    public class RatingWebService : IRatingWebService
    {

        private readonly IRatingRepository _ratingRepository;

        public RatingWebService()
        {
            _ratingRepository = new RatingRepository(new BzaleDatabaseContext());
        }
        public bool CreateRating(RatingDTO viewmodel)
        {
            try
            {
                Rating newrating = Mapper.Map<RatingDTO, Rating>(viewmodel);
                _ratingRepository.AddRating(newrating);
                return true;

            }
            catch (Exception ex)
            {

                throw;
                return false;
            }
        }

        public RatingDTO GetMostPositiveRatingForCompany(int companyID)
        {
            try
            {
                var allratings = _ratingRepository.GetRatingsForCompany(companyID);
                var mostpositive = allratings.Aggregate((i1, i2) => i1.Votes >= i2.Votes ? i1 : i2);
                RatingDTO viewmodel = Mapper.Map<Rating, RatingDTO>(mostpositive);
                return viewmodel;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public List<RatingDTO> GetRatingsForCompany(int companyID, int page, int size)
        {
            try
            {
                var allratings = _ratingRepository.GetRatingsForCompany(companyID).ToList();
                return allratings.Select(e => Mapper.Map<Rating, RatingDTO>(e)).ToList();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public bool RemoveRating(int id)
        {
            try
            {
                _ratingRepository.RemoveRating(id);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool UpdateRating(RatingDTO viewmodel)
        {
            try
            {
                Rating updatedrating = Mapper.Map<RatingDTO, Rating>(viewmodel);
                _ratingRepository.UpdateRating(updatedrating);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
