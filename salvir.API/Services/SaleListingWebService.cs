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
using System.Linq;

namespace deprosa.WebService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "SaleListingService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select SaleListingService.svc or SaleListingService.svc.cs at the Solution Explorer and start debugging.
    public class SaleListingWebService : ISaleListingWebService
    {
    

        private GenericRepository<SaleListing> _saleListingRepository;
        private GenericRepository<MainCategory> _mainCategoryRepository;
        private GenericRepository<ProductType> _productRepository;
        private GenericRepository<Manufacturer> _manufacturerRepository;
        private GenericRepository<Account> _accountRepository;
        private SubscriptionService _subscriptionService;
        private GenericRepository<Subscription> _subscriptionRepository;
        private ImageService _imageService;
        private CreateAndUpdateService _createAndUpdateService;
        public SaleListingWebService( )
        {
            BzaleDatabaseContext context = new BzaleDatabaseContext();
            _saleListingRepository = new GenericRepository<SaleListing>(context);
            _mainCategoryRepository = new GenericRepository<MainCategory>(context);
            _productRepository = new GenericRepository<ProductType>(context);
            _manufacturerRepository = new GenericRepository<Manufacturer>(context);
            _accountRepository = new GenericRepository<Account>(context);
            _subscriptionService = new SubscriptionService();
            _subscriptionRepository = new GenericRepository<Subscription>(context);
            _imageService = new ImageService();
            _createAndUpdateService = new CreateAndUpdateService();
        }


        public bool CreateNewSaleListing(SaleListingDTO model)
        {
            try
            {
                Account acc = _accountRepository.GetSingle(e=>e.ID == model.CreatedBy.ID);
                if (acc.Company !=null && !string.IsNullOrEmpty(acc.Company.VAT))
                {
                    var sale = Mapper.Map<SaleListingDTO, SaleListing>(model);
                    var product = _productRepository.GetSingle(e=> e.ID == model.ProductType.ID);
                    var salelisting = _createAndUpdateService.CreateSaleListingObject(sale, acc, product);
                    salelisting = _saleListingRepository.Add(salelisting);
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

        public List<SaleListingDTO> GetSaleListingsForCompany(int companyID, string sort, bool isAsc, int page, int size)
        {
            try
            {
                var salelistingsQuery = _saleListingRepository.Get(e=>e.CreatedBy.CompanyID == companyID && e.Deleted == null);
                var salelistings = Filter(salelistingsQuery, sort, isAsc, page, size).ToList();
                return salelistings.Select(Mapper.Map<SaleListing, SaleListingDTO>).ToList();

            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<SaleListingDTO> GetSaleListingsForCategory(int id, string sort, bool isAsc, int page, int size)
        {
            try
            {
                var salelistingsQuery = _saleListingRepository.Get(e=>e.ProductType.Category.ID == id && e.Deleted == null);
                var salelistings = Filter(salelistingsQuery, sort, isAsc, page, size).ToList();

                return salelistings.Select(Mapper.Map<SaleListing, SaleListingDTO>).ToList();

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public List<SaleListingDTO> GetSaleListingsBySearchString(string search, string sort, bool isAsc, int page, int size)
        {

            var salelistingsQuery = _saleListingRepository.Get(e=>e.Description.Contains(search) || e.Title.Contains(search));
            var salelistings = Filter(salelistingsQuery, sort, isAsc, page, size).ToList();
            return salelistings.Select(Mapper.Map<SaleListing, SaleListingDTO>).ToList();
        }

        #region Images
        public bool AddImageSaleListing(SaleListingDTO viewmodel, ImageUploadDTO img)
        {
            try
            {
                if (_imageService.ValidateExtension(img.FileName))
                {
                    //string imgurl = _imageService.SaveImageToFolder(img);
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
                    //_imageService.RemoveImageFromFolder(image.ImageURL);
                    _saleListingRepository.Update(salelisting);
                    //_log.LogSaleListing(salelisting.Owner.ID, salelisting.ID, eLogSaleListingType.Update);
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
                //_log.LogSaleListing(salelisting.Owner.ID, salelisting.ID, eLogSaleListingType.Comment);
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
                //_log.LogSaleListing(salelisting.Owner.ID, salelisting.ID, eLogSaleListingType.Comment);
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
                    //_log.LogSaleListing(salelisting.Owner.ID, salelisting.ID, eLogSaleListingType.Comment);
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

        IQueryable<SaleListing> Filter(IQueryable<SaleListing> salelistings, string sort, bool isAsc, int page, int size)
        {
            IQueryable<SaleListing> filtered = salelistings;
            switch (sort)
            {
                case "Title":
                        filtered = isAsc? salelistings.OrderBy(e => e.Title) : salelistings.OrderByDescending(e => e.Title);
                    break;
                case "Price":
                    filtered = isAsc ? salelistings.OrderBy(e => e.Price) : salelistings.OrderByDescending(e => e.Price);
                    break;
                case "Crated":
                    filtered = isAsc ? salelistings.OrderBy(e => e.Created) : salelistings.OrderByDescending(e => e.Created);
                    break;
                    
                default:
                   filtered = salelistings.OrderBy(e => e.Created);
                    break;
            }
            return filtered.Skip(size * page).Take(size);
        }
        #endregion
    }

}
