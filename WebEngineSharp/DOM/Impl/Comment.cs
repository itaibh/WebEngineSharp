using System;

namespace WebEngineSharp.DOM.Impl
{
    public class Comment : CharacterData, IComment
    {
        public Comment(IDocument doc, string comment)
            : base(doc, comment)
        {
        }
    }
}

