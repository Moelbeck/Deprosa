using deprosa.Model.Base;

namespace deprosa.Model
{
    public class CommentAnswer :BaseComment
    {
        public virtual Comment ParentComment { get; set; }
    }
}
