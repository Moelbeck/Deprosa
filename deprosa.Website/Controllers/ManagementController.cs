using System;
using System.Web.Mvc;
using deprosa.Common;
using deprosa.ViewModel;
using Common.Extension;
using System.Threading.Tasks;
using deprosa.Filter;
using deprosa.Service;
using deprosa.Web.Data.Model.ViewModel;
using deprosa.Web.Model;
using deprosa.Website;
using deprosa.Website.Controllers;
using deprosa.Website.Data.Model.ViewModel;
using deprosa.WebsiteService;
using deprosa.WebService;
using PagedList;
using VATChecker;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace deprosa.Web.Controllers
{
    [EnsureLoggedInAuthorize]
    public class ManagementController : BaseController
    {
        private AccountWebService _accountservice;
        //private VatValidationService _vatvalidationservice;
        private SalelistingWebService _saleListingService;
        public ManagementController()
        {
            _accountservice = new AccountWebService();
            _saleListingService = new SalelistingWebService();
        }
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult CompanyManagement()
        {
            CompanyDTO company = null;
            if (CurrentUser.IsCompanyValidated)
            {
               company =  _accountservice.GetCompanyInformation(CurrentUser.VAT);
                ViewBag.IsValidated = true;
            }
            else
            {
                ViewBag.IsValidated = false;
            }
            return View(company);
        }

        [HttpGet]
        public  ActionResult AccountManagement()
        {
            var accountinfo =  _accountservice.GetAccountInformation(CurrentUser.ID);
            return View(accountinfo);
        }

        [HttpGet]
        public ActionResult CompanySaleListings(string sort="", string search="", int? page=null)
        {
            int nextpage = Sort(sort, search, page);
            if (page != null)
            {
                nextpage = (int) page;
            }
            if (!string.IsNullOrEmpty(search))
            {
                nextpage = 1;
            }

            var salelistings =   _saleListingService.GetForCompany(CurrentUser.VAT, sort, nextpage, search);
            SaleListingListViewModel saleListingList = new SaleListingListViewModel
            {
                Salelistings = salelistings.ToPagedList(nextpage,int.MaxValue)
            };
            return View(saleListingList);
        }

        [HttpGet]
        public ActionResult FollowingSalelistings(string sort = "", string search = "", int? page = null)
        {
            int nextpage = Sort(sort, search, page);

            if (page != null)
            {
                nextpage = (int)page;
            }
            if (search != null)
            {
                nextpage = 1;
            }
            var salelistings =  _saleListingService.GetFollowingSalelistings(CurrentUser.ID, sort, nextpage, search);
            SaleListingListViewModel saleListingList = new SaleListingListViewModel
            {
                Salelistings = salelistings.ToPagedList(nextpage, int.MaxValue)
            };
            return View(saleListingList);
        }

        #region Account and company posts
        [HttpPost]
        public ActionResult ValidateVat(eCountryCode countrycode, string vat)
        {
            var cc = countrycode.EnumToString();
            CompanyDTO dto = null;
            if (!string.IsNullOrEmpty(cc) && cc.Length < 3)
            {
                var vatvalidation =  _accountservice.GetValidateVAT(cc,vat);
                if (vatvalidation.IsValid)
                {
                    ViewBag.IsValidated = true;
                    dto = CreateCompanyFromVatValidation(vatvalidation);
                }
            }
            return PartialView("CompanyManagement", dto);
        }

        [HttpPost]
        public ActionResult UpdateAccountInformation(AccountUpdateDTO updatedto)
        {
            AccountUpdateDTO updated = null;
            if (ModelState.IsValid)
            {
                updated =  _accountservice.UpdateAccountInformation(updatedto);
                return PartialView("AccountManagement", updated);

            }

            return PartialView("AccountManagement");
        }
        [HttpPost]
        public ActionResult AddOrUpdateCompany(CompanyDTO company)
        {
            if (ModelState.IsValid)
            {
                CompanyDTO savedOrUpdatedDTO = null;
                if (!CurrentUser.IsCompanyValidated)
                {
                    savedOrUpdatedDTO =  _accountservice.AddCompanyToAccount(CurrentUser.ID, company);
                }
                else
                {
                    savedOrUpdatedDTO = _accountservice.UpdateCompanyInformation(CurrentUser.ID, company);
                }
                ViewBag.IsValidated = true;
                CurrentUser.AddCompanyInformation(savedOrUpdatedDTO);
                return PartialView("CompanyManagement", savedOrUpdatedDTO);
            }
            return PartialView("CompanyManagement");
        }
        #endregion
        private CompanyDTO CreateCompanyFromVatValidation(ViesVatCheck dto) 
        {
            CompanyDTO company = null;
            //1:Adresse, 2: postnummer, 3: by
            string[] adressinfo = dto.Address.Split('\n');
            var postalcodeandCity = adressinfo[1].Split(new string[] { @" " }, StringSplitOptions.None);
            company = new CompanyDTO
            {
                VAT = dto.VATNumber,
                Name = dto.Name,
                Address = adressinfo[0],
                PostalCode = int.Parse(postalcodeandCity[0]),
                City = postalcodeandCity[1],
                CountryCode = dto.CountryCode.StringToEnum<eCountryCode>()
            };
            return company;
        }

    }
}
