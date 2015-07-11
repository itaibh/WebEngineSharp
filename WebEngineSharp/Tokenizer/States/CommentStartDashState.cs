using System;
using System.Collections.Specialized;
using System.IO;

namespace WebEngineSharp.Tokenizer.States
{
    // 8.4.2.47 http://www.w3.org/TR/html5/syntax.html#comment-start-dash-state
    public class CommentStartDashState : BaseState
    {
        private CommentStartDashState()
        {
        }

        private static CommentStartDashState s_Instance = new CommentStartDashState();

        public static CommentStartDashState Instance
        {
            get { return s_Instance; }
        }

        public override BaseState Process(HtmlTokenizer tokenizer, StreamReader reader)
        {
            int c = Read(reader);
            switch (c)
            {
                case '-':
                    break;
                case 0:
                    break;
                case '>':
                    break;
                case -1:
                    break;
                default:
                    break;
            }
            throw new NotImplementedException();
        }
    }
}
