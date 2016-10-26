using deprosa.Model;

namespace deprosa.Interfaces
{
    public interface ICompanyRepository
    {
        Company GetCompany(string vat);
        Company GetCompany(int id);

        Company AddNewCompany(Company newCompany);

        bool IsVatInDatabase(string vat);


        Company UpdateCompany(Company updatedCompany);

        bool IsEmailInDatabase(string email);
    }
}
