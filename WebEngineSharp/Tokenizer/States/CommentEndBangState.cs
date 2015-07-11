using System;
using System.Collections.Specialized;
using System.IO;

namespace WebEngineSharp.Tokenizer.States
{
    // 8.2.4.51 http://www.w3.org/TR/html5/syntax.html#comment-end-bang-state
    public class CommentEndBangState : BaseState
    {
        private CommentEndBangState()
        {
        }

        private static CommentEndBangState s_Instance = new CommentEndBangState();

        public static CommentEndBangState Instance
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
                    Token.Comment += "--!";
                    CommentEndDashState.Instance.Token = Token;
                    return CommentEndDashState.Instance;

                case '>':
                    tokenizer.EmitToken(Token);
                    return DataState.Instance;

                case 0:
                    ReportParseError();
                    Token.Comment += "--!\uFFFD";
                    CommentState.Instance.Token = Token;
                    return CommentState.Instance;

                case -1:
                    ReportParseError();
                    tokenizer.EmitToken(Token);
                    return DataState.Instance;
                    //Reconsume the EOF character (?)

                default:
                    Token.Comment += "--!" + (char)c;
                    CommentState.Instance.Token = Token;
                    return CommentState.Instance;
            }
        }
    }



}
