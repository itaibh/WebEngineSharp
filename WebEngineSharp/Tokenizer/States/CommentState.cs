using System;
using System.Collections.Specialized;
using System.IO;

namespace WebEngineSharp.Tokenizer.States
{
    // 8.2.4.48 http://www.w3.org/TR/html5/syntax.html#comment-state
    public class CommentState : BaseState
    {
        private CommentState()
        {
        }

        private static CommentState s_Instance = new CommentState();

        public static CommentState Instance
        {
            get { return s_Instance; }
        }

        public CommentToken Token { get; set; }

        public override BaseState Process(HtmlTokenizer tokenizer, StreamReader reader)
        {
            for (;;)
            {
                int c = Read(reader);
                switch (c)
                {
                    case '-':
                        CommentEndDashState.Instance.Token = Token;
                        return CommentEndDashState.Instance;

                    case 0:
                        ReportParseError();
                        Token.Comment += "\uFFFD";
                        break;

                    case -1:
                        ReportParseError();
                        tokenizer.EmitToken(Token);
                        return DataState.Instance;
                        //Reconsume the EOF character (?)

                    default:
                        Token.Comment += (char)c;
                        break;
                }
            }
        }
    }
}
