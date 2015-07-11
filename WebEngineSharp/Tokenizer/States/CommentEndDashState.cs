using System;
using System.Collections.Specialized;
using System.IO;

namespace WebEngineSharp.Tokenizer.States
{
    // 8.2.4.49 http://www.w3.org/TR/html5/syntax.html#comment-end-dash-state
    public class CommentEndDashState: BaseState
    {
        private CommentEndDashState()
        {
        }

        private static CommentEndDashState s_Instance = new CommentEndDashState();

        public static CommentEndDashState Instance
        {
            get { return s_Instance; }
        }

        public CommentToken Token { get; set; }

        public override BaseState Process(HtmlTokenizer tokenizer, StreamReader reader)
        {
            int c = Read(reader);
            switch(c){
                case '-':
                    CommentEndState.Instance.Token = Token;
                    return CommentEndState.Instance;

                case 0:
                    ReportParseError();
                    Token.Comment += "-\uFFFD";
                    CommentState.Instance.Token = Token;
                    return CommentState.Instance;

                case -1:
                    ReportParseError();
                    tokenizer.EmitToken(Token);
                    return DataState.Instance;
                    //Reconsume the EOF character (?)

                default:
                    Token.Comment += "-" + (char)c;
                    CommentState.Instance.Token = Token;
                    return CommentState.Instance;
            }
        }
	}

}
