using deprosa.ViewModel;
using System.Web;

namespace deprosa.Web.Model
{
    public static class CurrentUser 
    {
        public static int ID
        {
            get
            {
                int id = 0;
                if (HttpContext.Current.Session["id"] != null)
                {
                    id = (int)HttpContext.Current.Session["id"];

                }
                return id;
            }
            set
            {
                HttpContext.Current.Session["id"] = value;
            }
        }
        public static string Email
        {
            get
            {
                string mail = null;
                if (HttpContext.Current.Session["email"] != null)
                {
                    mail = HttpContext.Current.Session["email"] as string;
                }
                return mail;
            }
            set
            {
                HttpContext.Current.Session["email"] = value;
            }
        }
        public static string FirstName
        {
            get
            {
                string firstname = null;
                if ( HttpContext.Current.Session["firstname"] != null)
                {
                    firstname = HttpContext.Current.Session["firstname"] as string;
                }
                return firstname;
            }
            set
            {
                HttpContext.Current.Session["firstname"] = value;
            }
        }
        public static string LastName
        {
            get
            {
                string lastname = null;
                if (HttpContext.Current.Session["lastname"] != null)
                {
                    lastname = HttpContext.Current.Session["lastname"] as string;
                }
                return lastname;
            }
            set
            {
                HttpContext.Current.Session["lastname"] = value;
            }
        }
        #region CompanyInformation
        public static string VAT
        {
            get
            {
                string vat = null;
                if (HttpContext.Current.Session["vat"] != null)
                {
                    vat = HttpContext.Current.Session["vat"] as string;
                }
                return vat;
            }
            set
            {
                HttpContext.Current.Session["vat"] = value;
            }
        }

        public static string CompanyName
        {
            get
            {
                string companyinfo= null;
                if (HttpContext.Current.Session["companyname"] != null)
                {
                    companyinfo = (string)HttpContext.Current.Session["companyname"];
                }
                return companyinfo;
            }
            set
            {
                HttpContext.Current.Session["companyname"] = value;
            }
        }

        public static string CompanyAdress
        {
            get
            {
                string companyinfo = null;
                if (HttpContext.Current.Session["companyadress"] != null)
                {
                    companyinfo = (string)HttpContext.Current.Session["companyadress"];
                }
                return companyinfo;
            }
            set
            {
                HttpContext.Current.Session["companyadress"] = value;
            }
        }
        public static int CompanyPostalCode
        {
            get
            {
                int companyinfo = 0;
                if (HttpContext.Current.Session["companypostal"] != null)
                {
                    companyinfo = (int)HttpContext.Current.Session["companypostal"];
                }
                return companyinfo;
            }
            set
            {
                HttpContext.Current.Session["companypostal"] = value;
            }
        }
        public static string CompanyCity
        {
            get
            {
                string companyinfo = null;
                if (HttpContext.Current.Session["companycity"] != null)
                {
                    companyinfo = (string)HttpContext.Current.Session["companycity"];
                }
                return companyinfo;
            }
            set
            {
                HttpContext.Current.Session["companycity"] = value;
            }
        }

        public static string CompanyPhone
        {
            get
            {
                string companyinfo = null;
                if (HttpContext.Current.Session["companyPhone"] != null)
                {
                    companyinfo = (string)HttpContext.Current.Session["companyPhone"];
                }
                return companyinfo;
            }
            set
            {
                HttpContext.Current.Session["companyPhone"] = value;
            }
        }
        public static string CompanyEmail
        {
            get
            {
                string companyinfo = null;
                if (HttpContext.Current.Session["companyEmail"] != null)
                {
                    companyinfo = (string)HttpContext.Current.Session["companyEmail"];
                }
                return companyinfo;
            }
            set
            {
                HttpContext.Current.Session["companyEmail"] = value;
            }
        }

        public static bool IsCompanyValidated
        {
            get
            {
                return (!string.IsNullOrEmpty(VAT));
            }
        }
        #endregion


        public static void AddCompanyInformation(CompanyDTO company)
        {
            VAT = company.VAT;
            CompanyName = company.Name;
            CompanyAdress = company.Address;
            CompanyCity = company.City;
            CompanyPostalCode = company.PostalCode;
            CompanyPhone = company.Phone;
            CompanyEmail = company.Email;
        }

        public static void InstantiateCurrentUser(AccountDTO acc)
        {
            ID = acc.ID;
            FirstName = acc.FirstName;
            LastName = acc.LastName;
            Email = acc.Email;
            var company = acc.Company;
            if (company != null)
            {
                VAT = company.VAT;
                CompanyName = company.Name;
                CompanyAdress = company.Address;
                CompanyCity = company.City;
                CompanyPostalCode = company.PostalCode;
                CompanyPhone = company.Phone;
                CompanyEmail = company.Email;
            }
        }

        public static void SetCurrentUserToNull()
        {
            ID = 0;
            FirstName = null;
            LastName = null;
            Email = null;

            VAT =           null;
            CompanyName =   null;
            CompanyAdress = null;
            CompanyCity = null; 
            CompanyPostalCode = 0;
            CompanyPhone = null;
            CompanyEmail = null;
        }
    }
}
