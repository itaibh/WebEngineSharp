using System;
using System.Collections.Specialized;
using System.IO;

namespace WebEngineSharp.Tokenizer.States
{
    // 8.2.4.50 http://www.w3.org/TR/html5/syntax.html#comment-end-state
    public class CommentEndState: BaseState
    {
        private CommentEndState()
        {
        }

        private static CommentEndState s_Instance = new CommentEndState();

        public static CommentEndState Instance
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
                    case '>':
                        tokenizer.EmitToken(Token);
                        return DataState.Instance;

                    case 0:
                        ReportParseError();
                        Token.Comment += "--\uFFFD";
                        CommentState.Instance.Token = Token;
                        return CommentState.Instance;

                    case '!':
                        ReportParseError();
                        CommentEndBangState.Instance.Token = Token;
                        return CommentEndBangState.Instance;

                    case '-':
                        ReportParseError();
                        Token.Comment += "-";
                        break;

                    case -1:
                        ReportParseError();
                        tokenizer.EmitToken(Token);
                        return DataState.Instance;
                //Reconsume the EOF character (?)

                    default:
                        ReportParseError();
                        Token.Comment += "--" + (char)c;
                        CommentState.Instance.Token = Token;
                        return CommentState.Instance;
                }
            }
        }
    }
}
