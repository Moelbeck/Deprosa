using System.ComponentModel.DataAnnotations.Schema;

namespace deprosa.Model.Base
{
    /// <summary>
    /// Comments can belong to both a rating and a sale listings 
    /// </summary>
    [Table("Comments")]
    public abstract class BaseComment : Entity
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public virtual Account Sender { get; set; }
    }
}
