using depross.Common;
using depross.Model.Base;
using System.Collections.Generic;

namespace depross.Model
{
    public class Comment : BaseComment
    {

        public bool IsPrivateMessage { get; set; }

        public eCommentType CommentType { get; set; }

        public virtual List<CommentAnswer> Answers { get; set; }
    }
}
