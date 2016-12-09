using deprosa.Model.Base;

namespace deprosa.Model.Log
{
    public class LogCategory : BaseLog
    {
        public int MainCategoryId { get; set; }
        public int SubCategoryId { get; set; }  
    }
}
