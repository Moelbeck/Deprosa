using depross.Model;

namespace depross.Interfaces
{
    public interface IAccountRepository
    {
        Account GetAccount(string email);

        Account GetAccount(int id);

        Account AddNewAccount(Account newAccount);

        bool IsMailInDatabase(string email);

        Account UpdateAccount(Account updatedAccount);

        void DeleteAccount(int id);

    }
}
