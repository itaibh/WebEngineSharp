using System;
using System.Collections.Specialized;
using System.IO;
using System.Text;

namespace WebEngineSharp.Tokenizer.States
{
    // 8.2.4.44 http://www.w3.org/TR/html5/syntax.html#bogus-comment-state
    public class BogusCommentState : BaseState
    {
        private BogusCommentState()
        {
            Comment = new StringBuilder(4096);
        }

        private static BogusCommentState s_Instance = new BogusCommentState();

        public static BogusCommentState Instance
        {
            get { return s_Instance; }
        }

        public override BaseState Process(HtmlTokenizer tokenizer, StreamReader reader)
        {
            throw new NotImplementedException();
        }

        public StringBuilder Comment { get; private set; }
    }
}
