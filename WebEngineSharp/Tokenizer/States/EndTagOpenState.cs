using System;
using System.Collections.Specialized;
using System.IO;

namespace WebEngineSharp.Tokenizer.States
{
    // 8.2.4.9 http://www.w3.org/TR/html5/syntax.html#end-tag-open-state
    public class EndTagOpenState : BaseState
    {
        private EndTagOpenState()
        {
        }

        private static EndTagOpenState s_Instance = new EndTagOpenState();

        public static EndTagOpenState Instance
        {
            get { return s_Instance; }
        }

        public override BaseState Process(HtmlTokenizer tokenizer, StreamReader reader)
        {
            int c = Read(reader);
            if (base.IsUppercaseAsciiLetter(c))
            {
                EndTagToken token = new EndTagToken();
                token.TagName = Char.ToLower((char)c).ToString();
                TagNameState.Instance.Token = token;
                return TagNameState.Instance;
            } 

            if (base.IsLowercaseAsciiLetter(c))
            {
                EndTagToken token = new EndTagToken();
                token.TagName = ((char)c).ToString();
                TagNameState.Instance.Token = token;
                return TagNameState.Instance;
            } 

            if (c == '>')
            {
                ReportParseError();
                return DataState.Instance;
            }

            if (c == -1)
            {
                ReportParseError();
                return DataState.Instance;
            }

            ReportParseError();
            return BogusCommentState.Instance;
        }
    }
}
