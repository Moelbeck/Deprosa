using System;
using System.Linq;
using AutoMapper;
using deprosa.Model;
using deprosa.ViewModel;

namespace biz2biz.Service.Automapper
{
    public class BzaleAutoMapper : Profile
    {
        public BzaleAutoMapper()
        {
            ConfigureUserMapping();
        }

        private void ConfigureUserMapping()
        {
            Mapper.Initialize(m =>
            {
                #region Account
                m.CreateMap<Account, AccountDTO>()

                .ForMember(e => e.ID, o => o.MapFrom(account => account.ID))
                .ForMember(e => e.FirstName, o => o.MapFrom(account => account.FirstName))
                .ForMember(e => e.LastName, o => o.MapFrom(account => account.LastName))
                .ForMember(e => e.Phone, o => o.MapFrom(account => account.Phone))
                .ForMember(e => e.PostalCode, o => o.MapFrom(account => account.PostalCode))
                .ForMember(e => e.Address, o => o.MapFrom(account => account.Address))
                .ForMember(e => e.Gender, o => o.MapFrom(e => e.Gender))
                .ForMember(e => e.Email, o => o.MapFrom(account => account.Email))
                .ForMember(e=>e.CountryCode,o=>o.MapFrom(f=>f.CountryCode))
                .ForAllOtherMembers(e => e.Ignore());

                m.CreateMap<AccountDTO, Account>().ForMember(e => e.Gender, o => o.MapFrom(e => e.Gender.ToString()))
                .ForMember(e => e.ID, o => o.MapFrom(account => account.ID))
                .ForMember(e => e.FirstName, o => o.MapFrom(account => account.FirstName))
                .ForMember(e => e.LastName, o => o.MapFrom(account => account.LastName))
                .ForMember(e => e.Phone, o => o.MapFrom(account => account.Phone))
                .ForMember(e => e.PostalCode, o => o.MapFrom(account => account.PostalCode))
                .ForMember(e => e.Address, o => o.MapFrom(account => account.Address))
                .ForMember(e => e.Email, o => o.MapFrom(account => account.Email))
                .ForAllOtherMembers(e => e.Ignore());

                m.CreateMap<AccountCreateDTO, Account>()
                .ForMember(e => e.Email, o => o.MapFrom(x => x.Email))

                .ForMember(e => e.FirstName, o => o.MapFrom(x => x.FirstName))
                .ForMember(e => e.LastName, o => o.MapFrom(x => x.LastName))
                .ForMember(e => e.Created, o => o.MapFrom(s => DateTime.Now))
                .ForMember(e => e.FirstName, o => o.MapFrom(x => x.FirstName))
                .ForMember(e => e.FirstName, o => o.MapFrom(x => x.FirstName))
                .ForMember(e => e.FirstName, o => o.MapFrom(x => x.FirstName))
                .ForMember(e => e.FirstName, o => o.MapFrom(x => x.FirstName))
                .ForAllOtherMembers(e => e.Ignore());

                m.CreateMap<Account, AccountCreateDTO>()
                .ForMember(e => e.Email, o => o.MapFrom(x => x.Email))

                .ForMember(e => e.FirstName, o => o.MapFrom(x => x.FirstName))
                .ForMember(e => e.LastName, o => o.MapFrom(x => x.LastName))
                .ForAllOtherMembers(e => e.Ignore());

                m.CreateMap<Account, AccountUpdateDTO>()
                .ForMember(e => e.ID, o => o.MapFrom(s => s.ID))
                .ForMember(e => e.Email, o => o.MapFrom(x => x.Email))
                .ForMember(e => e.FirstName, o => o.MapFrom(s => s.FirstName))
                .ForMember(e => e.LastName, o => o.MapFrom(s => s.LastName))
                .ForMember(e => e.PostalCode, o => o.MapFrom(s => s.PostalCode))
                .ForMember(e => e.Gender, o => o.MapFrom(s => s.Gender))
                .ForMember(e => e.CountryCode, o => o.MapFrom(s => s.CountryCode))
                .ForMember(e => e.City, o => o.MapFrom(s => s.City))
                .ForMember(e => e.Address, o => o.MapFrom(s => s.Address))

                .ForAllOtherMembers(e => e.Ignore());

                m.CreateMap<AccountUpdateDTO, Account>()
                .ForMember(e => e.ID, o => o.MapFrom(s => s.ID))
                .ForMember(e => e.Email, o => o.MapFrom(x => x.Email))
                .ForMember(e => e.FirstName, o => o.MapFrom(s => s.FirstName))
                .ForMember(e => e.LastName, o => o.MapFrom(s => s.LastName))
                .ForMember(e => e.PostalCode, o => o.MapFrom(s => s.PostalCode))
                .ForMember(e => e.Gender, o => o.MapFrom(s => s.Gender))
                .ForMember(e => e.CountryCode, o => o.MapFrom(s => s.CountryCode))
                .ForMember(e => e.City, o => o.MapFrom(s => s.City))
                .ForMember(e => e.Address, o => o.MapFrom(s => s.Address))
                .ForAllOtherMembers(e => e.Ignore());

                m.CreateMap<Company, CompanyDTO>()
                .ForMember(e => e.ID, o => o.MapFrom(s => s.ID))
                .ForMember(e => e.VAT, o => o.MapFrom(s => s.VAT))
                .ForMember(e => e.Image, o => o.MapFrom(s => new ImageDTO { ImageURL = s.Image.ImageURL, ID = s.Image.ID }))
                .ForMember(e => e.Name, o => o.MapFrom(s => s.Name))
                .ForMember(e => e.Phone, o => o.MapFrom(s => s.Phone))
                .ForMember(e => e.PostalCode, o => o.MapFrom(s => s.PostalCode))
                .ForMember(e => e.Email, o => o.MapFrom(s => s.Email))
                .ForMember(e => e.City, o => o.MapFrom(s => s.City))
                .ForMember(e => e.Address, o => o.MapFrom(s => s.Address))
                .ForMember(e => e.CountryCode, o => o.MapFrom(s => s.CountryCode))
                .ForAllOtherMembers(e => e.Ignore());

                m.CreateMap<CompanyDTO, Company>()
                .ForMember(e => e.ID, o => o.MapFrom(s => s.ID))
                .ForMember(e => e.VAT, o => o.MapFrom(s => s.VAT))
                .ForMember(e => e.Image, o => o.MapFrom(s => new Image { ImageURL = s.Image.ImageURL, ID = s.Image.ID }))
                .ForMember(e => e.Name, o => o.MapFrom(s => s.Name))
                .ForMember(e => e.Phone, o => o.MapFrom(s => s.Phone))
                .ForMember(e => e.PostalCode, o => o.MapFrom(s => s.PostalCode))
                .ForMember(e => e.Email, o => o.MapFrom(s => s.Email))
                .ForMember(e => e.City, o => o.MapFrom(s => s.City))
                .ForMember(e => e.Address, o => o.MapFrom(s => s.Address))
                .ForMember(e => e.CountryCode, o => o.MapFrom(s => s.CountryCode))
                .ForAllOtherMembers(e => e.Ignore());
                #endregion

                #region Comment
                m.CreateMap<Comment, CommentDTO>()
                .ForMember(e => e.ID, o => o.MapFrom(s => s.ID))
                .ForMember(e => e.Message, o => o.MapFrom(s => s.Message))
                .ForMember(e => e.Title, o => o.MapFrom(s => s.Title))
                .ForMember(e => e.SenderID, o => o.MapFrom(s => s.Sender.ID))
                .ForMember(e => e.SenderFirstName, o => o.MapFrom(s => s.Sender.FirstName))
                .ForMember(e => e.SenderLastName, o => o.MapFrom(s => s.Sender.LastName))
                .ForMember(e => e.SenderEmail, o => o.MapFrom(s => s.Sender.Email))
                .ForMember(e => e.IsPrivateMessage, o => o.MapFrom(s => s.IsPrivateMessage))
                .ForAllOtherMembers(e => e.Ignore());


                m.CreateMap<CommentDTO, Comment>()
                .ForMember(e => e.ID, o => o.MapFrom(s => s.ID))
                .ForMember(e => e.Message, o => o.MapFrom(s => s.Message))
                .ForMember(e => e.Title, o => o.MapFrom(s => s.Title))
                .ForMember(e => e.Sender, o => o.MapFrom(s => new Account { ID = s.SenderID, FirstName = s.SenderFirstName, LastName = s.SenderLastName, Email = s.SenderEmail }))
                .ForMember(e => e.IsPrivateMessage, o => o.MapFrom(s => s.IsPrivateMessage))
                .ForAllOtherMembers(e => e.Ignore());

                m.CreateMap<CommentAnswer, CommentDTO>()
                .ForMember(e => e.ID, o => o.MapFrom(s => s.ID))
                .ForMember(e => e.Message, o => o.MapFrom(s => s.Message))
                .ForMember(e => e.Title, o => o.MapFrom(s => s.Title))
                .ForMember(e => e.SenderID, o => o.MapFrom(s => s.Sender.ID))
                .ForMember(e => e.SenderFirstName, o => o.MapFrom(s => s.Sender.FirstName))
                .ForMember(e => e.SenderLastName, o => o.MapFrom(s => s.Sender.LastName))
                .ForMember(e => e.SenderEmail, o => o.MapFrom(s => s.Sender.Email))
                .ForAllOtherMembers(e => e.Ignore());


                m.CreateMap<CommentDTO, CommentAnswer>()
                .ForMember(e => e.ID, o => o.MapFrom(s => s.ID))
                .ForMember(e => e.Message, o => o.MapFrom(s => s.Message))
                .ForMember(e => e.Title, o => o.MapFrom(s => s.Title))
                .ForMember(e => e.Sender, o => o.MapFrom(s => new Account { ID = s.SenderID, FirstName = s.SenderFirstName, LastName = s.SenderLastName, Email = s.SenderEmail }))
                .ForAllOtherMembers(e => e.Ignore());

                #endregion

                #region Category Mapping
                m.CreateMap<MainCategory, MainCategoryDTO>()
                                .ForMember(e => e.ID, o => o.MapFrom(x => x.ID))
                    .ForMember(e => e.Name, o => o.MapFrom(x => x.Name))
                    .ForMember(e => e.Description, o => o.MapFrom(x => x.Description))
                    .ForMember(e => e.Image, o => o.MapFrom(s => new ImageDTO { ID = s.Image.ID, ImageURL = s.Image.ImageURL }))
                .ForAllOtherMembers(e => e.Ignore());

                m.CreateMap<MainCategoryDTO, MainCategory>()
                .ForMember(e => e.ID, o => o.MapFrom(x => x.ID))
                .ForMember(e => e.Name, o => o.MapFrom(x => x.Name))
                .ForMember(e => e.Description, o => o.MapFrom(x => x.Description))
                .ForAllOtherMembers(e => e.Ignore());

                m.CreateMap<SubCategory, SubCategoryDTO>()
                .ForMember(e => e.ID, o => o.MapFrom(x => x.ID))
                .ForMember(e => e.MainCategory, o => o.MapFrom(x => new MainCategory { ID = x.MainCategory.ID, Name = x.MainCategory.Name, Description = x.MainCategory.Description}))
                .ForMember(e => e.Name, o => o.MapFrom(x => x.Name))
                .ForMember(e => e.Description, o => o.MapFrom(x => x.Description))
                .ForAllOtherMembers(e => e.Ignore());

                m.CreateMap<SubCategoryDTO, SubCategory>()
                                .ForMember(e => e.ID, o => o.MapFrom(x => x.ID))
                                .ForMember(e => e.Name, o => o.MapFrom(x => x.Name))
                .ForMember(e => e.MainCategory, o => o.MapFrom(x => new MainCategoryDTO { ID = x.MainCategory.ID, Name = x.MainCategory.Name, Description = x.MainCategory.Description }))

                .ForMember(e => e.Description, o => o.MapFrom(x => x.Description))
            .ForAllOtherMembers(e => e.Ignore());
                #endregion

                #region Sale listing Mapping               

                m.CreateMap<SaleListing, SaleListingDTO>()
                                    .ForMember(e => e.ID, o => o.MapFrom(s => s.ID))
                                    .ForMember(e => e.Title, o => o.MapFrom(s => s.Title))
                                    .ForMember(e => e.Description, o => o.MapFrom(s => s.Description))
                                    .ForMember(e => e.Price, o => o.MapFrom(s => s.Price))
                                    .ForMember(e => e.Condition, o => o.MapFrom(s => s.Condition))
                                    .ForMember(e => e.ExpirationDate, o => o.MapFrom(s => s.ExpirationDate))
                                    .ForMember(e => e.Height, o => o.MapFrom(s => s.Height))
                                    .ForMember(e => e.Width, o => o.MapFrom(s => s.Width))
                                    .ForMember(e => e.Depth, o => o.MapFrom(s => s.Depth))
                                    .ForMember(e => e.Weight, o => o.MapFrom(s => s.Weight))
                                    .ForMember(e => e.Thickness, o => o.MapFrom(s => s.Thickness))
                                    .ForMember(e => e.Length, o => o.MapFrom(s => s.Length))
                                    .ForMember(e => e.CPU, o => o.MapFrom(s => s.CPU))
                                    .ForMember(e => e.RAM, o => o.MapFrom(s => s.RAM))
                                    .ForMember(e => e.ScreenSize, o => o.MapFrom(s => s.ScreenSize))
                                    .ForMember(e => e.Harddisk, o => o.MapFrom(s => s.Harddisk))
                                    .ForMember(e => e.Model, o => o.MapFrom(s => s.Model))
                                    .ForMember(e => e.Year, o => o.MapFrom(s => s.Year))
                                    .ForMember(e => e.Kilometers, o => o.MapFrom(s => s.Kilometers))
                                    .ForMember(e => e.FuelType, o => o.MapFrom(s => s.FuelType))
                                    .ForMember(e => e.KmPrLiter, o => o.MapFrom(s => s.KmPrLiter))
                                    .ForMember(e => e.Color, o => o.MapFrom(s => s.Color))
                                    .ForMember(e => e.LastService, o => o.MapFrom(s => s.LastService))
                                    .ForMember(e => e.NoOfDoors, o => o.MapFrom(s => s.NoOfDoors))
                                    .ForMember(e => e.VatPayed, o => o.MapFrom(s => s.VatPayed))
                    .ForMember(e => e.Owner, o => o.MapFrom(s => new CompanyDTO { ID = s.Owner.ID, Name = s.Owner.Name }))
                .ForMember(e => e.ProductType, o => o.MapFrom(s => new ProductTypeDTO { ID = s.ProductType.ID, Name = s.ProductType.Name }))
                    .ForMember(e => e.CreatedBy, o => o.MapFrom(s => new AccountDTO { ID = s.CreatedBy.ID, FirstName = s.CreatedBy.FirstName, LastName = s.CreatedBy.LastName }))
                      .ForMember(e => e.Images, o => o.MapFrom(s => (s.Images.Select(b => new ImageDTO { ImageURL = b.ImageURL, ID = b.ID }))))
                .ForMember(e => e.Manufacturer, o => o.MapFrom(s => new ManufacturerDTO { ID = s.Manufacturer.ID, Name = s.Manufacturer.Name }))

                                      .ForAllOtherMembers(e => e.Ignore());


                m.CreateMap<SaleListingDTO, SaleListing>()
.ForMember(e => e.ID, o => o.MapFrom(s => s.ID))
                                    .ForMember(e => e.Title, o => o.MapFrom(s => s.Title))
                                    .ForMember(e => e.Description, o => o.MapFrom(s => s.Description))
                                    .ForMember(e => e.Price, o => o.MapFrom(s => s.Price))
                                    .ForMember(e => e.Condition, o => o.MapFrom(s => s.Condition))
                                    .ForMember(e => e.ExpirationDate, o => o.MapFrom(s => s.ExpirationDate))
                                    .ForMember(e => e.Height, o => o.MapFrom(s => s.Height))
                                    .ForMember(e => e.Width, o => o.MapFrom(s => s.Width))
                                    .ForMember(e => e.Depth, o => o.MapFrom(s => s.Depth))
                                    .ForMember(e => e.Weight, o => o.MapFrom(s => s.Weight))
                                    .ForMember(e => e.Thickness, o => o.MapFrom(s => s.Thickness))
                                    .ForMember(e => e.Length, o => o.MapFrom(s => s.Length))
                                    .ForMember(e => e.CPU, o => o.MapFrom(s => s.CPU))
                                    .ForMember(e => e.RAM, o => o.MapFrom(s => s.RAM))
                                    .ForMember(e => e.ScreenSize, o => o.MapFrom(s => s.ScreenSize))
                                    .ForMember(e => e.Harddisk, o => o.MapFrom(s => s.Harddisk))
                                    .ForMember(e => e.Model, o => o.MapFrom(s => s.Model))
                                    .ForMember(e => e.Year, o => o.MapFrom(s => s.Year))
                                    .ForMember(e => e.Kilometers, o => o.MapFrom(s => s.Kilometers))
                                    .ForMember(e => e.FuelType, o => o.MapFrom(s => s.FuelType))
                                    .ForMember(e => e.KmPrLiter, o => o.MapFrom(s => s.KmPrLiter))
                                    .ForMember(e => e.Color, o => o.MapFrom(s => s.Color))
                                    .ForMember(e => e.LastService, o => o.MapFrom(s => s.LastService))
                                    .ForMember(e => e.NoOfDoors, o => o.MapFrom(s => s.NoOfDoors))
                                    .ForMember(e => e.VatPayed, o => o.MapFrom(s => s.VatPayed))
                    .ForMember(e => e.Owner, o => o.MapFrom(s => new Company { ID = s.Owner.ID, Name = s.Owner.Name }))
                .ForMember(e => e.ProductType, o => o.MapFrom(s => new ProductType { ID = s.ProductType.ID, Name = s.ProductType.Name }))
                    .ForMember(e => e.CreatedBy, o => o.MapFrom(s => new Account { ID = s.CreatedBy.ID, FirstName = s.CreatedBy.FirstName, LastName = s.CreatedBy.LastName }))
                      .ForMember(e => e.Images, o => o.MapFrom(s => (s.Images.Select(b => new Image { ImageURL = b.ImageURL, ID = b.ID }))))
                .ForMember(e => e.Manufacturer, o => o.MapFrom(s => new Manufacturer { ID = s.Manufacturer.ID, Name = s.Manufacturer.Name }))
                                      .ForAllOtherMembers(e => e.Ignore());
                #endregion
                #region Image
                m.CreateMap<Image, ImageDTO>()

                    .ForMember(e => e.ID, o => o.MapFrom(s => s.ID))
                    .ForMember(e => e.ImageURL, o => o.MapFrom(s => s.ImageURL))
                .ForAllOtherMembers(e => e.Ignore());

                m.CreateMap<ImageDTO, Image>()
                .ForMember(e => e.ID, o => o.MapFrom(s => s.ID))
                .ForMember(e => e.ImageURL, o => o.MapFrom(s => s.ImageURL))
                .ForAllOtherMembers(e => e.Ignore());
                #endregion
                #region Product

                m.CreateMap<ProductType, ProductTypeDTO>()
                    .ForMember(e => e.ID, o => o.MapFrom(s => s.ID))
                    .ForMember(e => e.Name, o => o.MapFrom(s => s.Name))
                    .ForMember(e => e.SubCategoryID, o => o.MapFrom(s => s.Category.ID))
                    .ForMember(e => e.SubCategory, o => o.MapFrom(s => new SubCategoryDTO { ID = s.Category.ID, Name = s.Category.Name,
                        MainCategory = new MainCategoryDTO { ID = s.Category.MainCategory.ID, Name = s.Category.MainCategory.Name}
                    }))
                    .ForMember(e => e.Types, o => o.MapFrom(s => s.Types))
                    .ForMember(e => e.Types, o => o.MapFrom(s => s.Types))
                                    .ForAllOtherMembers(e => e.Ignore());

                m.CreateMap<ProductTypeDTO, ProductType>()
                    .ForMember(e => e.ID, o => o.MapFrom(s => s.ID))
                    .ForMember(e => e.Category, o => o.MapFrom(s => new SubCategory { ID = s.SubCategoryID}))

                    .ForMember(e => e.Types, o => o.MapFrom(s => s.Types))

                    .ForMember(e => e.Name, o => o.MapFrom(s => s.Name))
                .ForAllOtherMembers(e => e.Ignore());
                #endregion

                #region Manufacturer
                m.CreateMap<ManufacturerDTO, Manufacturer>()
                .ForMember(e => e.Name, o => o.MapFrom(s => s.Name))
                .ForMember(e => e.Description, o => o.MapFrom(s => s.Description))
                                .ForAllOtherMembers(e => e.Ignore());
                m.CreateMap<Manufacturer, ManufacturerDTO>()
                .ForMember(e => e.Name, o => o.MapFrom(s => s.Name))
                .ForMember(e => e.Description, o => o.MapFrom(s => s.Description))
                .ForAllOtherMembers(e => e.Ignore());
                #endregion

                #region Rating

                m.CreateMap<Rating, RatingDTO>()
                    .ForMember(e => e.ID, o => o.MapFrom(s => s.ID))
                    .ForMember(e => e.Votes, o => o.MapFrom(s => s.Votes))
                    .ForMember(e => e.CompanyID, o => o.MapFrom(s => s.Company.ID))
                    .ForMember(e => e.Description, o => o.MapFrom(s => s.Description))
                    .ForMember(e => e.GivenRating, o => o.MapFrom(s => s.GivenRating))
                    .ForMember(e => e.CompanyImage, o => o.MapFrom(s => new ImageDTO { ImageURL = s.Company.Image != null ? s.Company.Image.ImageURL : null }))
                                    .ForAllOtherMembers(e => e.Ignore());

                m.CreateMap<RatingDTO, Rating>()
    .ForMember(e => e.ID, o => o.MapFrom(s => s.ID))
    .ForMember(e => e.Votes, o => o.MapFrom(s => s.Votes))
    .ForMember(e => e.Company, o => o.MapFrom(s => new Company { ID = s.CompanyID }))
    .ForMember(e => e.Description, o => o.MapFrom(s => s.Description))
    .ForMember(e => e.GivenRating, o => o.MapFrom(s => s.GivenRating))
                    .ForAllOtherMembers(e => e.Ignore());

                #endregion

            });

            Mapper.AssertConfigurationIsValid();

        }
    }
}
