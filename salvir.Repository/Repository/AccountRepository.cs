using System;
using depross.Repository.Abstract;
using depross.Repository.DatabaseContext;
using depross.Interfaces;
using depross.Model;

namespace depross.Repository
{
    public class AccountRepository : GenericRepository<Account>, IAccountRepository
    {
        public AccountRepository() : this(new BzaleDatabaseContext())
        {

        }
        public AccountRepository(BzaleDatabaseContext context) : base(context)
        {

        }
        public Account GetAccount(string email)
        {
            Account account = GetSingle(e => e.Email.Equals(email, StringComparison.CurrentCultureIgnoreCase) && e.Deleted == null);
            return account;
        }

        public Account GetAccount(int id)
        {
            Account account = GetSingle(e => e.ID == id && e.Deleted == null);
            return account;
        }

        public Account AddNewAccount(Account newAccount)
        {
            var added = Add(newAccount);
            Save();
            return added;
        }

        public bool IsMailInDatabase(string email)
        {
            return GetSingle(e => e.Email == email) != null;
        }

        public Account UpdateAccount(Account updatedAccount)
        {
            Edit(updatedAccount);
            Save();
            return GetSingle(e => e.ID == updatedAccount.ID);
        }

        public void DeleteAccount(int id)
        {
            var account = GetSingle(e => e.ID == id);
            account.Deleted = DateTime.Now;
            Save();
        }
    }
}
