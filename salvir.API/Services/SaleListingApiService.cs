using AutoMapper;
using deprosa.Common;
using deprosa.Interfaces;
using deprosa.Model;
using deprosa.Repository;
using deprosa.Repository.Abstract;
using deprosa.Repository.DatabaseContext;
using deprosa.service;
using deprosa.Service;
using deprosa.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace deprosa.WebService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "SaleListingService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select SaleListingService.svc or SaleListingService.svc.cs at the Solution Explorer and start debugging.
    public class SaleListingApiService : ISaleListingWebService
    {
        private readonly GenericRepository<SaleListing> _saleListingRepository;
        private readonly GenericRepository<ProductType> _productRepository;
        private readonly GenericRepository<Account> _accountRepository;
        private readonly SubscriptionService _subscriptionService;
        private readonly GenericRepository<Subscription> _subscriptionRepository;
        private readonly ImageService _imageService;
        private readonly CreateAndUpdateService _createAndUpdateService;
        public SaleListingApiService( )
        {
            BzaleDatabaseContext context = new BzaleDatabaseContext();
            _saleListingRepository = new GenericRepository<SaleListing>(context);
            _productRepository = new GenericRepository<ProductType>(context);
            _accountRepository = new GenericRepository<Account>(context);
            _subscriptionService = new SubscriptionService();
            _subscriptionRepository = new GenericRepository<Subscription>(context);
            _imageService = new ImageService();
            _createAndUpdateService = new CreateAndUpdateService();
        }


        public bool CreateNewSaleListing(SaleListingCreateDTO model, int userid)
        {
            try
            {
                Account acc = _accountRepository.Get(e=>e.ID == userid && e.Deleted == null).Include(e=>e.Company).Single();
                ProductType producttype = _productRepository.Get(e=>e.ID == model.ProductType.ID && e.Deleted == null).Single();
                if (acc.Company !=null && !string.IsNullOrEmpty(acc.Company.VAT))
                {
                    var sale = Mapper.Map<SaleListingCreateDTO, SaleListing>(model);
                    sale.CreatedBy = acc;
                    sale.Owner = acc.Company;
                    sale.AccountId = acc.ID;
                    sale.ProductType = producttype;
                    sale.ProductTypeId = producttype.ID;
                    var salelisting = _createAndUpdateService.CreateSaleListingObject(sale);
                    _saleListingRepository.Add(salelisting);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool DeleteSaleListingByID(int saleID)
        {
            try
            {
                _saleListingRepository.Delete(saleID);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool UpdateSaleListing(SaleListingDTO viewmodel)
        {
            try
            {
                SaleListing updated = Mapper.Map<SaleListingDTO, SaleListing>(viewmodel);
                SaleListing current = GetSale(viewmodel.ID);
                current = _createAndUpdateService.UpdateSaleListingFields(current, updated);
                _saleListingRepository.Update(current);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public SaleListingDTO GetSaleListingByID(int id)
        {
            try
            {
                var salelisting = GetSale(id);
                SaleListingDTO viewmodelmodel = Mapper.Map<SaleListing, SaleListingDTO>(salelisting);
                return viewmodelmodel;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<SaleListingDTO> GetPopular(List<int> logged, int categoryid, bool issub)
        {
            var salelistings =_saleListingRepository.Get(e => logged.Contains(e.ID) && e.Deleted == null && !e.IsSold).ToList();
            if (!salelistings.Any())
            {
                if (issub)
                {
                    salelistings = _saleListingRepository.Get(e => 
                    !e.IsSold && e.ProductType.Category.ID == categoryid).Include(e=>e.ProductType.Category)
                       .OrderByDescending(o => o.Created)
                        .Take(5)
                        .ToList();
                }
                else
                {
                    salelistings = _saleListingRepository.Get(e => !e.IsSold && 
                    e.ProductType.Category.MainCategory.ID == categoryid)
                    .Include(e => e.ProductType.Category.MainCategory)
                        .OrderByDescending(o => o.Created)
                        .Take(5)
                        .ToList();
                }
            }
            return salelistings.Select(Mapper.Map<SaleListing, SaleListingDTO>).ToList();
        }
        public List<SaleListingDTO> GetForCompany(string vat, string sort, int page, int size, string search)
        {
            try
            {
                IQueryable<SaleListing> salelistingsQuery;
                if (!string.IsNullOrWhiteSpace(search))
                {
                    salelistingsQuery =_saleListingRepository.Get(e => e.Owner.VAT.Equals(vat) && e.Deleted == null && !e.IsSold && e.Description.Contains(search) || e.Title.Contains(search)).Include(i=>i.Owner);
                }
                else
                {
                    salelistingsQuery = _saleListingRepository.Get(e => e.Owner.VAT.Equals(vat) && e.Deleted == null);
                }
                var salelistings = Filter(salelistingsQuery, sort, page, size).ToList();
                return salelistings.Select(Mapper.Map<SaleListing, SaleListingDTO>).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<SaleListingDTO> GetFollowingSalelistings(int userid, string sort, int page, int size, string search)
        {
            try
            {
                IQueryable<SaleListing> salelistingsQuery;
                var allsalelistings = _accountRepository.Get(e => e.ID == userid).SelectMany(s => s.Following);
                if (!string.IsNullOrWhiteSpace(search))
                {

                    salelistingsQuery = allsalelistings.Where(e => e.Deleted == null && !e.IsSold && e.Description.Contains(search) || e.Title.Contains(search));
                }
                else
                {
                    salelistingsQuery = allsalelistings.Where(e => !e.IsSold &&  e.Deleted == null);
                }
                var salelistings = Filter(salelistingsQuery, sort, page, size).ToList();
                return salelistings.Select(Mapper.Map<SaleListing, SaleListingDTO>).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<SaleListingDTO> GetForSubCategory(int id, string sort, int page, int size, string search)
        {
            try
            {
                IQueryable<SaleListing> salelistingsQuery;
                if (!string.IsNullOrWhiteSpace(search))
                {
                    salelistingsQuery =
                        _saleListingRepository.Get(
                            e => e.ProductType.Category.ID == id && e.Deleted == null && !e.IsSold && e.Description.Contains(search) || e.Title.Contains(search));

                }
                else
                {
                    salelistingsQuery = _saleListingRepository.Get(e=>e.ProductType.Category.ID == id && e.Deleted == null && !e.IsSold);
                }
                var salelistings = Filter(salelistingsQuery, sort, page, size).ToList();
                return salelistings.Select(Mapper.Map<SaleListing, SaleListingDTO>).ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public List<SaleListingDTO> GetForProductType(int id, string sort, int page, int size, string search)
        {
            try
            {
                IQueryable<SaleListing> salelistingsQuery;
                if (!string.IsNullOrWhiteSpace(search))
                {
                    salelistingsQuery =
                        _saleListingRepository.Get(
                            e => e.ProductType.ID == id && e.Deleted == null && !e.IsSold && e.Description.Contains(search) || e.Title.Contains(search));

                }
                else
                {
                    salelistingsQuery = _saleListingRepository.Get(e => e.ProductType.ID == id && e.Deleted == null && !e.IsSold);
                }
                var salelistings = Filter(salelistingsQuery, sort, page, size).Include(i => i.ProductType.Category).ToList();
                return salelistings.Select(Mapper.Map<SaleListing, SaleListingDTO>).ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<SaleListingDTO> GetBySearchString(string search,  string sort, int page, int size)
        {

            var salelistingsQuery = _saleListingRepository.Get(e=>e.Description.Contains(search) || e.Title.Contains(search) && !e.IsSold && e.Deleted == null);
            var salelistings = Filter(salelistingsQuery, sort, page, size).ToList();
            return salelistings.Select(Mapper.Map<SaleListing, SaleListingDTO>).ToList();
        }

        #region Images
        public bool AddImageSaleListing(SaleListingDTO viewmodel, ImageUploadDTO img)
        {
            try
            {
                if (_imageService.ValidateExtension(img.FileName))
                {
                    Image image = new Image { Type = img.ImageType, ImageData = img.Content };
                    SaleListing salelisting = GetSale(viewmodel.ID);
                    salelisting.Images.Add(image);
                    _saleListingRepository.Update(salelisting);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool RemoveImageSaleListing(int salelistingid, int imageid)
        {
            try
            {
                SaleListing salelisting = GetSale(salelistingid);
                var image = salelisting.Images.FirstOrDefault(e => e.ID == imageid);
                if (image != null)
                {
                    salelisting.Images.Remove(image);
                    _saleListingRepository.Update(salelisting);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<ImageDTO> GetImagesForSaleListing(int salelistingid)
        {
            try
            {
                var images = GetSale(salelistingid)?.Images;
                return images.Select(Mapper.Map<Image, ImageDTO>).ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public ImageDTO GetImageForSaleListing(int salelistingid)
        {
            try
            {
                var images = GetSale(salelistingid)?.Images;
                Image mainimage = null;
                if (images.Any()) { mainimage = images[0]; }
                return Mapper.Map<Image, ImageDTO>(mainimage);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        #region Subscription
        public bool AddOrUpdateSubscription(eSubscription sub, SaleListingDTO salelistingviewmodel)
        {
            try
            {
                SaleListing salelisting = GetSale(salelistingviewmodel.ID);
                Subscription subscription = _subscriptionService.CreateSubscription(sub);
                _subscriptionRepository.Add(subscription);
                salelisting.Subscription = subscription;
                _saleListingRepository.Update(salelisting);
                //_log.LogSaleListing(salelisting.Owner.ID, salelisting.ID, eLogSaleListingType.Update);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion

        #region Comments
        public bool AddComment(int salelistingid, CommentDTO commentviewmodel)
        {
            try
            {
                SaleListing salelisting = GetSale(salelistingid);
                Comment comment = Mapper.Map<CommentDTO, Comment>(commentviewmodel);
                SaleListingComment(comment);
                salelisting.Comments.Add(comment);
                _saleListingRepository.Update(salelisting);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool AddAnswerForComment(int salelistingID, int commentID, CommentDTO answerviewmodel)
        {
            try
            {
                SaleListing salelisting = GetSale(salelistingID);
                Comment comment = salelisting.Comments.Single(e => e.ID == commentID);
               CommentAnswer answer = Mapper.Map<CommentDTO, CommentAnswer>(answerviewmodel);
                comment.Answers.Add(answer);
                _saleListingRepository.Update(salelisting);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool RemoveComment(SaleListingDTO saleviewmodel, int id)
        {
            try
            {
                SaleListing salelisting = GetSale(saleviewmodel.ID);

                var comment = salelisting.Comments.FirstOrDefault(e => e.ID == id);
                if (comment != null)
                {
                    salelisting.Comments.Remove(comment);
                    _saleListingRepository.Update(salelisting);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<CommentDTO> GetCommentsForSaleListing(int salelistingID)
        {
            try
            {
                SaleListing salelisting = GetSale(salelistingID);
                var comments = salelisting.Comments;
                List<CommentDTO> viewmodels = comments.Select(e => Mapper.Map<Comment, CommentDTO>(e)).ToList();

                return viewmodels;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        #region Private

        SaleListing GetSale(int id)
        {
            return _saleListingRepository.GetSingle(e => e.ID == id && e.Deleted == null);
        }
        void SaleListingComment(Comment comment)
        {
            comment.CommentType = eCommentType.SaleListing;
        }

        IQueryable<SaleListing> Filter(IQueryable<SaleListing> salelistings, string sort, int page, int size)
        {
            IQueryable<SaleListing> filtered = salelistings;
            sort = sort.ToLower();
            switch (sort)
            {
                case "title":
                         filtered = salelistings.OrderBy(e => e.Title);
                    break;
                case "title_desc":
                    filtered = salelistings.OrderByDescending(e => e.Title);
                    break;
                case "price":
                    filtered = salelistings.OrderBy(e => e.Price) ;
                    break;
                case "price_desc":
                    filtered = salelistings.OrderByDescending(e => e.Price);
                    break;
                case "created":
                    filtered = salelistings.OrderBy(e => e.Created);
                    break;
                case "created_desc":
                    filtered = salelistings.OrderByDescending(e => e.Created);
                    break;

                default:
                   filtered = salelistings.OrderBy(e => e.Created);
                    break;
            }
            return filtered.Skip(size * (page-1)).Take(size);
        }
        #endregion
    }

}
