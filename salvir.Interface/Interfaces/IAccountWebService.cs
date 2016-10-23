using depross.ViewModel;

namespace depross.Interfaces
{
    public interface IAccountWebtService
    {
        #region Account
        AccountDTO Login(AccountLoginDTO account);
        void Logout(string email);

        AccountDTO CreateNewAccount(AccountCreateDTO newaccount);

        AccountDTO AddAccountToCompany( int currentAccountId, AccountCreateDTO newaccount);
         
        bool IsEmailInDatabase(string email);
 
        AccountUpdateDTO GetAccountInformation(int id);
 
        AccountUpdateDTO UpdateAccountInformation(AccountUpdateDTO account); 

        bool UpdatePassword(AccountUpdatePasswordViewModel account);
        #endregion
        bool IsUserVerifiedToSell(AccountDTO viewmodel);

        bool DeleteAccount(int id);

        #region Company
        bool IsVatInDatabase(string vat);
         
        CompanyDTO GetCompanyInformation(string vat);

        CompanyDTO UpdateCompanyInformation(int currentuserId, CompanyDTO company);
        CompanyDTO AddCompanyToAccount(int currentUserId, CompanyDTO newCompany);

        bool IsCompanyEmailInDatabase(string email);
        #endregion
    }
}
