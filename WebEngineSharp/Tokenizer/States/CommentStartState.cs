using System;
using System.Collections.Specialized;
using System.IO;

namespace WebEngineSharp.Tokenizer.States
{
    // 8.2.4.46 http://www.w3.org/TR/html5/syntax.html#comment-start-state
    public class CommentStartState : BaseState
    {
        private CommentStartState()
        {
        }

        private static CommentStartState s_Instance = new CommentStartState();

        public static CommentStartState Instance
        {
            get { return s_Instance; }
        }

        public CommentToken Token { get; set; }

        public override BaseState Process(HtmlTokenizer tokenizer, StreamReader reader)
        {
            int c = Read(reader);
            switch (c)
            {
                case '-':
                    return CommentStartDashState.Instance;

                case 0:
                    ReportParseError();
                    Token.Comment += '\uFFFD';
                    CommentState.Instance.Token = Token;
                    return CommentState.Instance;

                case '>':
                    ReportParseError();
                    tokenizer.EmitToken(Token);
                    return DataState.Instance;

                case -1:
                    ReportParseError();
                    tokenizer.EmitToken(Token);
                    return DataState.Instance;
                    // Reconsume the EOF character (?)

                default:
                    Token.Comment += (char)c;
                    CommentState.Instance.Token = Token;
                    return CommentState.Instance;
            }
        }
    }
}
