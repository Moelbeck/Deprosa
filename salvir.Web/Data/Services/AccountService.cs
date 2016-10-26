using System.Threading.Tasks;
using deprosa.Service;
using deprosa.ViewModel;
using deprosa.Common;

namespace deprosa.WebsiteService
{
    /// <summary>
    /// AccountService is handling login, creation etc for accounts
    /// </summary>
    public class AccountService
    {
        private string accountURI = string.Format("{0}{1}",Konstanter.BASEURI, "Account/");
        private HttpBaseClient client;
        public AccountService()
        {
            client = new HttpBaseClient(accountURI);
        }

        #region GET

        public async Task<AccountDTO> Login(AccountLoginDTO accountlogin)
        {
            string uri = string.Format("login");
            var user = await client.GetResponseObject<AccountLoginDTO, AccountDTO>(uri, eHttpMethodType.POST, accountlogin);
            return user;
        }
        public async Task<bool> IsEmailInDatabase(string email)
        {
            var user = await client.GetResponseObject<bool, bool>(string.Format("{0}/ismailindatabase", email), eHttpMethodType.GET, false);
            return user;
        }

        public async Task<AccountUpdateDTO> GetAccountInformation(int id)
        {
            var user = await client.GetResponseObject<AccountUpdateDTO, AccountUpdateDTO>(string.Format("{0}/accountinfo", id), eHttpMethodType.GET, null);
            return user;
        }
        public async Task<bool> IsVatInDatabase(string vat)
        {
            var user = await client.GetResponseObject<bool, bool>(string.Format("{0}/isvatindatabase", vat), eHttpMethodType.GET, false);
            return user;
        }

        public async Task<CompanyDTO> GetCompanyInformation(string vat)
        {
            var user = await client.GetResponseObject<CompanyDTO, CompanyDTO>(string.Format("{0}/companyinfo", vat), eHttpMethodType.GET, null);
            return user;
        }
        public async Task<bool> IsCompanyEmailInDatabase(string email)
        {
            var user = await client.GetResponseObject<bool, bool>(string.Format("{0}/iscompanymailindatabase", email), eHttpMethodType.GET, false);
            return user;
        }

        #endregion

        #region POST
        public async Task<bool> Logout(AccountDTO account)
        {
            string uri = string.Format("logout");
            var isloggedout = await client.GetResponseObject<AccountDTO, bool>(uri, eHttpMethodType.POST, account);
            return isloggedout;
        }
        public async Task<AccountDTO> CreateAccount(AccountCreateDTO model)
        {
            var user = await client.GetResponseObject<AccountCreateDTO, AccountDTO>("create", eHttpMethodType.POST, model);
            return user;
        }

        public async Task<AccountDTO> AddAccountToCompany(int currentaccount, AccountCreateDTO model)
        {
            var user = await client.GetResponseObject<AccountCreateDTO, AccountDTO>(string.Format("{0}/add", currentaccount), eHttpMethodType.POST, model);
            return user;
        }

        public async Task<CompanyDTO> AddCompanyToAccount(int currentaccount, CompanyDTO model)
        {
            var user = await client.GetResponseObject<CompanyDTO, CompanyDTO>(string.Format("{0}/company/add", currentaccount), eHttpMethodType.POST, model);
            return user;
        }
        #endregion
        #region PUT
        public async Task<AccountUpdateDTO> UpdateAccountInformation(AccountUpdateDTO model)
        {
            var user = await client.GetResponseObject<AccountUpdateDTO, AccountUpdateDTO>(string.Format("updateaccountinformation"), eHttpMethodType.PUT, model);
            return user;
        }
        public async Task<CompanyDTO> UpdateCompanyInformation(CompanyUpdateRequest model)
        {
            var company = await client.GetResponseObject<CompanyUpdateRequest, CompanyDTO>(string.Format("updatecompanyinformation"), eHttpMethodType.PUT, model);
            return company;
        }
        public async Task<bool> UpdatePassword(AccountUpdatePasswordViewModel model)
        {
            var user = await client.GetResponseObject<AccountUpdatePasswordViewModel, bool>(string.Format("updatepassword"), eHttpMethodType.PUT, model);
            return user;
        }

        #endregion

        #region DELETE
        public async Task<bool> DeleteAccount(AccountDTO model)
        {
            var user = await client.GetResponseObject<bool, bool>(string.Format("delete?id={0}", model.ID), eHttpMethodType.DELETE, false);
            return user;
        }
        #endregion



    }
}
