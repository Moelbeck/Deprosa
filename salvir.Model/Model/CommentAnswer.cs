using depross.Model.Base;

namespace depross.Model
{
    public class CommentAnswer :BaseComment
    {
        public virtual Comment ParentComment { get; set; }
    }
}
