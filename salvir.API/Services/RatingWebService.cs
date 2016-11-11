using AutoMapper;
using deprosa.Interfaces;
using deprosa.Model;
using deprosa.Repository;
using deprosa.Repository.Abstract;
using deprosa.Repository.DatabaseContext;
using deprosa.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace deprosa.WebService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "RatingService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select RatingService.svc or RatingService.svc.cs at the Solution Explorer and start debugging.
    public class RatingWebService : IRatingWebService
    {

        private readonly GenericRepository<Rating> _ratingRepository;

        public RatingWebService()
        {
            BzaleDatabaseContext context = new BzaleDatabaseContext();
            _ratingRepository = new GenericRepository<Rating>(context);
        }
        public bool CreateRating(RatingDTO viewmodel)
        {
            try
            {
                Rating newrating = Mapper.Map<RatingDTO, Rating>(viewmodel);
                _ratingRepository.Add(newrating);
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public RatingDTO GetMostPositiveRatingForCompany(int companyID)
        {
            try
            {
                var allratings = _ratingRepository.Get(e=>e.Company.ID == companyID);
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
                var allratings = _ratingRepository.Get(e => e.Company.ID == companyID);
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
                _ratingRepository.Delete(id);
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
                _ratingRepository.Update(updatedrating);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
