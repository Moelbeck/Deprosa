using System;
using System.Web.Mvc;
using deprosa.Common;
using deprosa.ViewModel;
using Common.Extension;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using deprosa.Filter;
using deprosa.Web.Model;
using deprosa.WebsiteService;
using PagedList;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace deprosa.Web.Controllers
{
    [EnsureLoggedInAuthorize]
    public class ManagementController : Controller
    {
        private AccountService _accountservice;
        private VatValidationService _vatvalidationservice;
        private CategoryService _categoryService;
        private SaleListingService _saleListingService;
        public ManagementController()
        {
            _accountservice = new AccountService();
            _vatvalidationservice = new VatValidationService();
            _categoryService = new CategoryService();
            _saleListingService = new SaleListingService();
        }
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> CompanyManagement()
        {
            CompanyDTO company = null;
            if (CurrentUser.IsCompanyValidated)
            {
               company = await _accountservice.GetCompanyInformation(CurrentUser.VAT);
                ViewBag.IsValidated = true;
            }
            else
            {
                ViewBag.IsValidated = false;
            }
            return View(company);
        }

        [HttpGet]
        public async Task<ActionResult> AccountManagement()
        {
            var accountinfo = await _accountservice.GetAccountInformation(CurrentUser.ID);
            return View(accountinfo);
        }

        [HttpGet]
        public async Task<ActionResult> CompanySaleListings(string sort="", string search="", int? page=null)
        {
            ViewBag.CurrentSort = sort;
            ViewBag.DateSortParm = sort = String.IsNullOrWhiteSpace(sort) ? "created_desc" : "";
            ViewBag.TitleSortParam = sort == "title" ? "title_desc" : "title";
            ViewBag.PriceSortParm = sort == "price" ? "price_desc" : "price";
            int nextpage=0;
            if (page != null)
            {
                nextpage = (int) page;
            }
            if (search != null)
            {
                nextpage = 1;
            }

            var salelistings =  await _saleListingService.GetSaleListingsForCompany(CurrentUser.VAT, sort, nextpage, search);
            SaleListingListViewModel saleListingList = new SaleListingListViewModel
            {
                Salelistings = salelistings.ToPagedList(nextpage,int.MaxValue)
            };
            return View(saleListingList);
        }

        [HttpGet]
        public ActionResult SavedSaleListings()
        {
            return View();
        }

        #region Account and company posts
        [HttpPost]
        public async Task<ActionResult> ValidateVat(eCountryCode countrycode, string vat)
        {
            var cc = countrycode.EnumToString();
            CompanyDTO dto = null;
            if (!string.IsNullOrEmpty(cc) && cc.Length < 3)
            {
                var vatvalidation = await _vatvalidationservice.GetVatValidation(cc, vat);
                if (vatvalidation.IsValid)
                {
                    ViewBag.IsValidated = true;
                    dto = CreateCompanyFromVatValidation(vatvalidation);
                }
            }
            return PartialView("CompanyManagement", dto);
        }

        [HttpPost]
        public async Task<ActionResult> UpdateAccountInformation(AccountUpdateDTO updatedto)
        {
            AccountUpdateDTO updated = null;
            if (ModelState.IsValid)
            {
                updated = await _accountservice.UpdateAccountInformation(updatedto);
                return PartialView("AccountManagement", updated);

            }

            return PartialView("AccountManagement");
        }
        [HttpPost]
        public async Task<ActionResult> AddOrUpdateCompany(CompanyDTO company)
        {
            if (ModelState.IsValid)
            {
                CompanyDTO savedOrUpdatedDTO = null;
                if (!CurrentUser.IsCompanyValidated)
                {
                    savedOrUpdatedDTO = await _accountservice.AddCompanyToAccount(CurrentUser.ID, company);
                }
                else
                {
                    CompanyUpdateRequest request = new CompanyUpdateRequest
                    {
                        Company = company,
                        AccountID = CurrentUser.ID
                    };
                    savedOrUpdatedDTO = await _accountservice.UpdateCompanyInformation(request);
                }
                ViewBag.IsValidated = true;
                CurrentUser.AddCompanyInformation(savedOrUpdatedDTO);
                return PartialView("CompanyManagement", savedOrUpdatedDTO);
            }
            return PartialView("CompanyManagement");
        }
        #endregion
        private CompanyDTO CreateCompanyFromVatValidation(VatValidationDTO dto) 
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
