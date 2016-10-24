using AutoMapper;
using depross.Common;
using depross.Extension;
using depross.Interfaces;
using depross.Model;
using depross.Repository;
using depross.Repository.DatabaseContext;
using depross.service;
using depross.Service;
using depross.ViewModel;
using System;


namespace depross.WebService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    /// <summary>
    /// AccountService is handling login, creation etc for accounts
    /// </summary>
    public class AccountWebService : IAccountWebtService
    {
        readonly IAccountRepository _accountRepository;
        readonly ICompanyRepository _companyRepository;
        readonly CreateAndUpdateService _createAndUpdateService;

        public AccountWebService()
        {
            BzaleDatabaseContext context = new BzaleDatabaseContext();
            _accountRepository = new AccountRepository(context);
            _companyRepository = new CompanyRepository(context);
            _createAndUpdateService = new CreateAndUpdateService();
        }

        public AccountDTO Login(AccountLoginDTO user)
        {
            AccountDTO accountViewModel = null;
            try
            {
                if (!string.IsNullOrWhiteSpace(user.UserName))
                {
                    Account account = _accountRepository.GetAccount(user.UserName);
                    if (PasswordValidationService.GetInstance().ValidatePassword(user.Password, account.Password, account.Salt))
                    {
                        //_log.LogLoginLogout(account.ID, eLoginType.Login);
                        accountViewModel = Mapper.Map<Account, AccountDTO>(account);
                        if (account.Company != null)
                        {
                            var company = Mapper.Map<Company, CompanyDTO>(account.Company);
                            accountViewModel.Company = company;
                        }
                    }

                }
            }
            catch(Exception ex)
            {
                return null;
            }
            return accountViewModel;
        }
        public void Logout(string usermail)
        {
            try
            {
                //_log.LogLoginLogout(account.ID, eLoginType.Logout);
            }
            catch(Exception ex)
            {
                throw;
            }

        }
        public AccountDTO CreateNewAccount(AccountCreateDTO newaccount)
        {
            try { 
            if (newaccount.Email.IsValidEmail() && !IsEmailInDatabase(newaccount.Email))
            {
                Account account = Mapper.Map<AccountCreateDTO, Account>(newaccount);

                string salt = PasswordValidationService.GetInstance().GenerateSalt();
                account.Password = PasswordValidationService.GetInstance().GenerateCryptedPassword(newaccount.Password, salt);
                account.Salt = salt;
                account = _accountRepository.AddNewAccount(account);
                var accountviewmodel = Mapper.Map<Account, AccountDTO>(account);
                return accountviewmodel;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return null;
        }

        public AccountDTO AddAccountToCompany(int currentAccountId, AccountCreateDTO newaccount)
        {
            try { 
            var current = _accountRepository.GetAccount(currentAccountId);
                var currentcompany = current.Company;
                if (currentcompany != null)
                {
                    if ((current.Type == eAccountType.Owner || current.Type == eAccountType.Administrator) && IsVatInDatabase(currentcompany.VAT) && !IsEmailInDatabase(newaccount.Email))
                    {
                        Account account = Mapper.Map<AccountCreateDTO, Account>(newaccount);
                        Company company = currentcompany;
                        string salt = PasswordValidationService.GetInstance().GenerateSalt();
                        account.Password = PasswordValidationService.GetInstance().GenerateCryptedPassword(newaccount.Password, salt);
                        account.Salt = salt;
                        account.Company = _companyRepository.AddNewCompany(company);
                        account = _accountRepository.AddNewAccount(account);
                        var accountviewmodel = Mapper.Map<Account, AccountDTO>(account);
                        return Mapper.Map<Account, AccountDTO>(current);
                    }

                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return null;
        }

        public CompanyDTO AddCompanyToAccount(int currentuserID, CompanyDTO newCompany)
        {
            try { 
            var current = _accountRepository.GetAccount(currentuserID);
            if (!_companyRepository.IsVatInDatabase(newCompany.VAT))
            {
                var newcompany = _createAndUpdateService.CreateCompanyObject(newCompany);
                    var company = _companyRepository.AddNewCompany(newcompany);
                current.Company = company;
                    current.CompanyID = company.ID;
               var acc = _accountRepository.UpdateAccount(current);
                var viewmodel = Mapper.Map<Company, CompanyDTO>(acc.Company);

                return viewmodel;
            }
            }
            catch (Exception ex)
            {
                return null;
            }
            return null;
        }
        public bool IsEmailInDatabase(string email)
        {
            try
            {
                return _accountRepository.IsMailInDatabase(email.Trim().ToLower());

            }
            catch (Exception ex)
            {

                return false;
            }
        }
        public bool UpdatePassword(AccountUpdatePasswordViewModel accountviewmodel)
        {
            try
            {
                var account = _accountRepository.GetAccount(accountviewmodel.ID);
                string salt = PasswordValidationService.GetInstance().GenerateSalt();
                string oldpass = PasswordValidationService.GetInstance().GenerateCryptedPassword(accountviewmodel.OldPassword, account.Salt);
                if (oldpass.Equals(account.Password) && accountviewmodel.NewPassword.Equals(accountviewmodel.ConfirmedPassword))
                {
                    account.Password = PasswordValidationService.GetInstance().GenerateCryptedPassword(accountviewmodel.NewPassword, salt);
                    account.Salt = salt;
                    _accountRepository.UpdateAccount(account);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {

                return false;
            }
        }

        public bool IsUserVerifiedToSell(AccountDTO viewmodel)
        {
            try
            {
                Account acc = _accountRepository.GetAccount(viewmodel.ID);
                return (acc.Company != null && acc.Company.ID > 0 && acc.HasValidatedMail);

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public AccountUpdateDTO GetAccountInformation(int id)
        {
            try
            {

                Account ac = _accountRepository.GetAccount(id);
                var accountviewmodel = Mapper.Map<Account, AccountUpdateDTO>(ac);
                return accountviewmodel;
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public AccountUpdateDTO UpdateAccountInformation(AccountUpdateDTO viewmodel)
        {
            try
            {
                if (viewmodel.ID > 0)
                {
                    Account updatedacc = Mapper.Map<AccountUpdateDTO, Account>(viewmodel);

                    var currentaccount = _accountRepository.GetAccount(viewmodel.ID);
                    var updated = _createAndUpdateService.UpdateAccountFields(currentaccount, updatedacc);
                    updated = _accountRepository.UpdateAccount(updated);
                    var accountviewmodel = Mapper.Map<Account, AccountUpdateDTO>(updated);

                    return accountviewmodel;

                }
                return null;

            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public bool DeleteAccount(int id)
        {
            try
            {
                _accountRepository.DeleteAccount(id);
                return true;
            }
            catch ( Exception ex)
            {

                return false;
            }
        }

        public bool IsVatInDatabase(string vat)
        {
            try
            {
                return _companyRepository.IsVatInDatabase(vat);

            }
            catch (Exception ex)
            {

                throw;
            }        }

        public CompanyDTO GetCompanyInformation(string vat)
        {
            try
            {
                var company = _companyRepository.GetCompany(vat);
                CompanyDTO viewmodel = Mapper.Map<Company, CompanyDTO>(company);
                return viewmodel;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public bool IsCompanyEmailInDatabase(string email)
        {
            try
            {
                return _companyRepository.IsEmailInDatabase(email);
            }
            catch ( Exception ex)
            {

                throw;
            }
        }

        public CompanyDTO UpdateCompanyInformation(int currentuserId, CompanyDTO viewmodel)
        {
            try
            {
                var account = _accountRepository.GetAccount(currentuserId);
                CompanyDTO companyviewmodel=null;
                if (!string.IsNullOrWhiteSpace( viewmodel.VAT))
                {
                    Company updatedcompany = Mapper.Map<CompanyDTO, Company>(viewmodel);
                    var currentcompany = _companyRepository.GetCompany(viewmodel.VAT);
                    if (account.Company.VAT.Equals(currentcompany.VAT)) {
                        currentcompany = _createAndUpdateService.UpdateCompanyFields(currentcompany, updatedcompany);
                        currentcompany = _companyRepository.UpdateCompany(currentcompany);
                        companyviewmodel = Mapper.Map<Company, CompanyDTO>(currentcompany);
                    }
                    return companyviewmodel;
                }
                return null;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

    }
}
