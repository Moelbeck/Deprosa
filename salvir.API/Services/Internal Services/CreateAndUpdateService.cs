using deprosa.Extension;
using deprosa.Model;
using deprosa.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace deprosa.service
{
    public class CreateAndUpdateService
    {
        private static int DAYSBEFOREEXPIRING = 28;

        public Company CreateCompanyObject(CompanyDTO newcompany)
        {
            Company copy = new Company
            {
                Name = newcompany.Name,
                PostalCode = newcompany.PostalCode,
                Address = newcompany.Address,
                CountryCode = newcompany.CountryCode,
                Email = newcompany.Email,
                Phone = newcompany.Phone,
                VAT = newcompany.VAT,
                City = newcompany.City,
                Created = DateTime.Now
            };
            return copy;
        }

        public SaleListing CreateSaleListingObject(SaleListing newsalelisting, Account owner, ProductType product)
        {
            SaleListing salelisting = newsalelisting;
            salelisting.ExpirationDate = DateTime.Now.AddDays(DAYSBEFOREEXPIRING);
            salelisting.Owner = owner.Company;
            salelisting.ProductType = product;
            salelisting.Subscription = new Subscription(); //might handled otherwise.
            salelisting.Comments = new List<Comment>();
            return salelisting;

        }
        public Company UpdateCompanyFields(Company current, Company updated)
        {
            Company copy = new Company
            {
                ID = current.ID,
                Name = updated.Name.Trim().Any() ? updated.Name : current.Name,
                PostalCode = updated.PostalCode,
                Address = updated.Address,
                CountryCode = updated.CountryCode,
                City = updated.City,
                Email = updated.Email.IsValidEmail() ? updated.Email : current.Email,
                Phone = updated.Phone.IsValidPhoneNr() ? updated.Phone : current.Phone,
                VAT = updated.VAT
            };
            return copy;
        }

        public Account UpdateAccountFields(Account current, Account updated)
        {
            Account copy = new Account
            {
                ID = current.ID,
                FirstName =updated.FirstName,
                LastName = updated.LastName,
                Address = updated.Address ,
                Email = updated.Email.IsValidEmail() ? updated.Email : current.Email,
                Phone = updated.Phone.IsValidPhoneNr() ? updated.Phone : current.Phone,
                Gender = updated.Gender,
                Password = current.Password,
                Salt = current.Salt,
                Company = current.Company,
                HasValidatedMail = current.HasValidatedMail,
                CountryCode = updated.CountryCode,
                City = updated.City,
                PostalCode = updated.PostalCode
            };
            return copy;
        }

        /// <summary>
        /// Updates the fields for a SaleListing object, before it is calling the database.
        /// The reason is, that the viewmodel does not map reference property, like CreatedBy or Comments proberly.
        /// </summary>
        /// <returns></returns>
        public SaleListing UpdateSaleListingFields(SaleListing current, SaleListing updated)
        {
            SaleListing copy = new SaleListing
            {
                ID = current.ID,
                CreatedBy = current.CreatedBy,
                Comments = current.Comments,
                Images = current.Images, //Does not update images from this!
                Description = updated.Description,
                ExpirationDate = current.ExpirationDate,
                Price = updated.Price,
                Title = updated.Title,
                Owner = current.Owner,
                Subscription = current.Subscription,
                ProductType = updated.ProductType,
                Color = updated.Color,
                CPU = updated.CPU,
                Depth = updated.Depth,
                FuelType = updated.FuelType,
                Harddisk = updated.Harddisk,
                Height = updated.Height,
                Kilometers = updated.Kilometers,
                KmPrLiter = updated.KmPrLiter,
                LastService = updated.LastService,
                Manufacturer = updated.Manufacturer,
                Length = updated.Length,
                Model = updated.Model,
                NoOfDoors  = updated.NoOfDoors,
                RAM = updated.RAM,
                ScreenSize = updated.ScreenSize,
                Thickness = updated.Thickness,
                VatPayed = updated.VatPayed,
                Weight = updated.Weight,
                Width = updated.Width,
                Year = updated.Year
            };
            return copy;
        }
    }
}