using AutoMapper;
using deprosa.Common;
using deprosa.Extension;
using deprosa.Interfaces;
using deprosa.Model;
using deprosa.Repository;
using deprosa.Repository.Abstract;
using deprosa.Repository.DatabaseContext;
using deprosa.service;
using deprosa.Service;
using deprosa.ViewModel;
using System;
using System.Linq;

namespace deprosa.WebService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    /// <summary>
    /// AccountService is handling login, creation etc for accounts
    /// </summary>
    public class AccountWebService : IAccountWebtService
    {
        readonly GenericRepository<Account> _accountRepository;
        readonly GenericRepository<Company> _companyRepository;
        readonly CreateAndUpdateService _createAndUpdateService;

        public AccountWebService()
        {
            BzaleDatabaseContext context = new BzaleDatabaseContext();
            _accountRepository = new GenericRepository<Account>(context);
            _companyRepository = new GenericRepository<Company>(context);
            _createAndUpdateService = new CreateAndUpdateService();
        }

        public AccountDTO Login(AccountLoginDTO user)
        {
            AccountDTO accountViewModel = null;
            try
            {
                if (!string.IsNullOrWhiteSpace(user.UserName))
                {
                    Account account = _accountRepository.GetSingle(e => e.Email.ToLower() == user.UserName.ToLower());
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
            catch (Exception ex)
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
            catch (Exception ex)
            {
                throw;
            }

        }
        public AccountDTO CreateNewAccount(AccountCreateDTO newaccount)
        {
            try
            {
                if (newaccount.Email.IsValidEmail() && !IsEmailInDatabase(newaccount.Email))
                {
                    Account account = Mapper.Map<AccountCreateDTO, Account>(newaccount);

                    string salt = PasswordValidationService.GetInstance().GenerateSalt();
                    account.Password = PasswordValidationService.GetInstance().GenerateCryptedPassword(newaccount.Password, salt);
                    account.Salt = salt;
                    account = _accountRepository.Add(account);
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
            try
            {
                var current = GetAcc(currentAccountId);
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
                        account.Company = _companyRepository.Add(company);
                        account = _accountRepository.Add(account);
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
            try
            {
                var current = GetAcc(currentuserID);
                if (!IsVatInDatabase(newCompany.VAT))
                {
                    var newcompany = _createAndUpdateService.CreateCompanyObject(newCompany);
                    var company = _companyRepository.Add(newcompany);
                    current.Company = company;
                    current.CompanyID = company.ID;
                    _accountRepository.Update(current);
                    var viewmodel = Mapper.Map<Company, CompanyDTO>(company);

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
                return GetAcc(email) !=null;

            }
            catch (Exception ex)
            {

                return false;
            }
        }

        internal AccountDTO GetAccount(string username)
        {
            var acc = GetAcc(username);
            return Mapper.Map<Account, AccountDTO>(acc);
        }

        public bool UpdatePassword(AccountUpdatePasswordViewModel accountviewmodel)
        {
            try
            {
                var account = GetAcc(accountviewmodel.ID);
                string salt = PasswordValidationService.GetInstance().GenerateSalt();
                string oldpass = PasswordValidationService.GetInstance().GenerateCryptedPassword(accountviewmodel.OldPassword, account.Salt);
                if (oldpass.Equals(account.Password) && accountviewmodel.NewPassword.Equals(accountviewmodel.ConfirmedPassword))
                {
                    account.Password = PasswordValidationService.GetInstance().GenerateCryptedPassword(accountviewmodel.NewPassword, salt);
                    account.Salt = salt;
                    _accountRepository.Update(account);
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
                Account acc = GetAcc(viewmodel.ID);
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
                Account ac = GetAcc(id);
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

                    var currentaccount = GetAcc(viewmodel.ID);
                    var updated = _createAndUpdateService.UpdateAccountFields(currentaccount, updatedacc);
                    _accountRepository.Update(updated);
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
                _accountRepository.Delete(id);
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }

        public bool IsVatInDatabase(string vat)
        {
            return _companyRepository.Get(e => e.VAT == vat).Any();
        }

        public CompanyDTO GetCompanyInformation(string vat)
        {
            try
            {
                var company = _companyRepository.GetSingle(e=>e.VAT == vat && e.Deleted == null);
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
                return _companyRepository.Get(e=>e.Email.ToLower() == email.ToLower()).Any();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public CompanyDTO UpdateCompanyInformation(int currentuserId, CompanyDTO viewmodel)
        {
            try
            {
                var account = GetAcc(currentuserId);
                CompanyDTO companyviewmodel = null;
                if (!string.IsNullOrWhiteSpace(viewmodel.VAT))
                {
                    Company updatedcompany = Mapper.Map<CompanyDTO, Company>(viewmodel);
                    var currentcompany = _companyRepository.GetSingle(e=>e.VAT == viewmodel.VAT && e.Deleted == null);
                    if (account.Company.VAT.Equals(currentcompany.VAT))
                    {
                        currentcompany = _createAndUpdateService.UpdateCompanyFields(currentcompany, updatedcompany);
                        _companyRepository.Update(currentcompany);
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

        private Account GetAcc(int id)
        {
            return _accountRepository.GetSingle(e => e.ID == id && e.Deleted == null);
        }
        private Account GetAcc(string email)
        {
            return _accountRepository.GetSingle(e => e.Email.ToLower() == email.ToLower() && e.Deleted == null);
        }
    }
}
