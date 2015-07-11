using System;

namespace WebEngineSharp.Tokenizer
{
    public class CommentToken : BaseToken
    {
        public CommentToken()
        {
            Comment = string.Empty;
        }

        public string Comment { get; set; }

        public override string ToString()
        {
            return string.Format("[CommentToken: Comment={0}]", Comment);
        }
    }
}

