using deprosa.Model.Base;

namespace deprosa.Model.Base
{
    /// <summary>
    /// Comments can belong to both a rating and a sale listings 
    /// </summary>
    public abstract class BaseComment : Entity
    {
        public string Title { get; set; }
        public string Message { get; set; }

        public virtual Account Sender { get; set; }
    }
}
