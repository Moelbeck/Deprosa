using deprosa.ViewModel;

namespace deprosa.Common
{
    public class CompanyUpdateRequest
    {
        public int AccountID { get; set; }
        public CompanyDTO Company { get; set; }
    }
}
