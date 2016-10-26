using deprosa.Common;
using deprosa.Model.Base;
using System.Collections.Generic;

namespace deprosa.Model
{
    public class Comment : BaseComment
    {

        public bool IsPrivateMessage { get; set; }

        public eCommentType CommentType { get; set; }

        public virtual List<CommentAnswer> Answers { get; set; }
    }
}
