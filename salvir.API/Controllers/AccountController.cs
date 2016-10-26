using deprosa.Common;
using deprosa.ViewModel;
using deprosa.WebService;
using System.Web.Http;
using deprosa.API.Authenticator;

namespace WebService.Api.Controllers
{

    [RoutePrefix("api/Account")]
    public class AccountController : ApiController
    {
        private AccountWebService _accountService;

        public AccountController()
        {
            _accountService = new AccountWebService();
        }
        /// <summary>
        /// Login for an existing account - Post
        /// </summary>
        [HttpPost, Route("login")]
        public IHttpActionResult Login([FromBody] AccountLoginDTO accountlogin)
        {
            if (ModelState.IsValid)
            {
                var login = _accountService.Login(accountlogin);
                if (login != null)
                {
                    return Ok(login);
                }
                else
                {
                    return NotFound();
                }
            }
            return BadRequest(ModelState);
        }

        /// <summary>
        /// Logout, mostly for logging reasons - Post
        /// </summary>
        [HttpPost, Route("logout")]
        public IHttpActionResult Logout(string email)
        {
            _accountService.Logout(email);
            return Ok();
        }

        /// <summary>
        /// Create a new account - Post
        /// </summary>
        [HttpPost, Route("create")]
        public IHttpActionResult Register([FromBody] AccountCreateDTO newaccount)
        {
            if (ModelState.IsValid)
            {
                var acc = _accountService.CreateNewAccount(newaccount);
                if (acc != null)
                {
                    return Ok(acc);
                }
                else
                    return BadRequest("Bruger blev ikke lavet. Enten fejl af input eller server fejl");
            }
            return BadRequest(ModelState);
        }

        /// <summary>
        /// Add Account to company - Post
        /// </summary>
        [Authorize]
        [HttpPost, Route("{currentAccountId}/add")]
        public IHttpActionResult AddAccountToCompany(int currentAccountId, [FromBody]AccountCreateDTO newaccount)
        {
            if (ModelState.IsValid)
            {
                var acc = _accountService.AddAccountToCompany(currentAccountId, newaccount);
                if (acc != null)
                {
                    return Ok(acc);
                }
                else
                {
                    return BadRequest("Bruger blev ikke lavet");
                }
            }
            return BadRequest(ModelState);
        }

        /// <summary>
        /// Add company to Account - Post
        /// </summary>
        [EnsureLoggedInAuthorize]
        [HttpPost, Route("{currentUserId}/company/add")]
        public IHttpActionResult AddCompanyToAccount(int currentuserId, [FromBody]CompanyDTO newcompany)
        {
            if (ModelState.IsValid)
            {
                var company = _accountService.AddCompanyToAccount(currentuserId, newcompany);
                if (company != null)
                {
                    return Ok(company);
                }
                else
                {
                    return BadRequest("Virksomhed blev ikke lavet!");
                }
            }
            return BadRequest(ModelState);
        }

        /// <summary>
        /// Checks if email is in DB - Get
        /// </summary>
        [HttpGet,Route("{email}/ismailindatabase")]        
        public IHttpActionResult IsEmailInDatabase(string email)
        {
            if (_accountService.IsEmailInDatabase(email))
            {
                return Ok(true);
            }
            else
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Get Account information - Get
        /// </summary>
        [Authorize]
        [HttpGet, Route("{id}/accountinfo")]
        public IHttpActionResult GetAccountInformation(int id)
        {

            var info = _accountService.GetAccountInformation(id);
            if (info != null)
            {
                return Ok(info);
            }
            else
            {
                return NotFound();
            }

        }

        /// <summary>
        /// Update account information - Put
        /// </summary>
        [Authorize]
        [HttpPut, Route("updateaccountinformation")]        
        public IHttpActionResult UpdateAccountInformation([FromBody]AccountUpdateDTO viewmodel)
        {
            if (ModelState.IsValid)
            {
                var updated = _accountService.UpdateAccountInformation(viewmodel);
                if (updated != null)
                {
                    return Ok(updated);
                }
                else
                {
                    return BadRequest("Information blev ikke opdateret");
                }
            }
            return BadRequest(ModelState);
        }

        /// <summary>
        /// Update company information - Put
        /// </summary>
        [Authorize]
        [HttpPut, Route("updatecompanyinformation")]
        public IHttpActionResult UpdateCompanyInformation([FromBody]CompanyUpdateRequest viewmodel)
        {
            if (ModelState.IsValid)
            {
                var updated = _accountService.UpdateCompanyInformation(viewmodel.AccountID, viewmodel.Company);
                if (updated != null)
                {
                    return Ok(updated);
                }
                else
                {
                    return BadRequest("Information blev ikke opdateret");
                }
            }
            return BadRequest(ModelState);

        }

        /// <summary>
        /// Checks if vat is in db - Get
        /// </summary>
        [Authorize]
        [HttpGet, Route("{vat}/isvatindatabase")]
        public IHttpActionResult IsVatInDatabase(string vat)
        {
            if (ModelState.IsValid)
            {
                if (_accountService.IsVatInDatabase(vat))
                {
                    return Ok(true);
                }
                else
                {
                    return NotFound();
                }
            }
            return BadRequest(ModelState);
        }

        /// <summary>
        /// Get company information - Get
        /// </summary>
        [Authorize]
        [HttpGet, Route("{vat}/companyinfo")]
        public IHttpActionResult GetCompanyInformation(string vat)
        {
            var info = _accountService.GetCompanyInformation(vat);
            if (info != null)
            {
                return Ok(info);
            }
            else
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Checks if company email is in DB - Get
        /// </summary>
        [Authorize]
        [HttpGet, Route("{email}/iscompanymailindatabase")]
        public IHttpActionResult IsCompanyEmailInDatabase(string email)
        {

            if (_accountService.IsCompanyEmailInDatabase(email))
            {
                return Ok(true);
            }
            else
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Update Password - Post
        /// </summary>
        [Authorize]
        [HttpPost, Route("updatepassword")]
        public IHttpActionResult UpdatePassword([FromBody]AccountUpdatePasswordViewModel accountviewmodel)
        {
            if (ModelState.IsValid)
            {
                if (_accountService.UpdatePassword(accountviewmodel))
                {
                    return Ok(true);
                }
                return NotFound();
            }
            return BadRequest(ModelState);
        }

        /// <summary>
        /// Delete account - Delete
        /// </summary>
        [Authorize]
        [HttpDelete, Route("delete")]
        public IHttpActionResult DeleteAccount(int id)
        {
            if (ModelState.IsValid)
            {
                if (_accountService.DeleteAccount(id))
                {
                    return Ok(true);
                }
                return NotFound();
            }
            return BadRequest(ModelState);
        }
    }
}
