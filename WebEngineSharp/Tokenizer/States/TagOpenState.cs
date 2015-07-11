using System;
using System.Collections.Specialized;
using System.IO;

namespace WebEngineSharp.Tokenizer.States
{
    // 8.2.4.8 http://www.w3.org/TR/html5/syntax.html#tag-open-state
    public class TagOpenState : BaseState
    {
        private TagOpenState()
        {
        }

        private static TagOpenState s_Instance = new TagOpenState();

        public static TagOpenState Instance
        {
            get { return s_Instance; }
        }

        public override BaseState Process(HtmlTokenizer tokenizer, StreamReader reader)
        {
            char c = (char)Read(reader);
            if (c == '!')
            {
                return MarkupDeclarationOpenState.Instance;
            } 

            if (c == '/')
            {
                return EndTagOpenState.Instance;
            }

            if (base.IsUppercaseAsciiLetter(c))
            {
                StartTagToken token = new StartTagToken();
                token.TagName = Char.ToLower(c).ToString();
                TagNameState.Instance.Token = token;
                return TagNameState.Instance;
            } 

            if (base.IsLowercaseAsciiLetter(c))
            {
                StartTagToken token = new StartTagToken();
                token.TagName = ((char)c).ToString();
                TagNameState.Instance.Token = token;
                return TagNameState.Instance;
            } 

            if (c == '?')
            {
                ReportParseError();
                return BogusCommentState.Instance;
            }

            ReportParseError();
            tokenizer.EmitChar(c);
            DataState.Instance.LastConsumedCharacters.Enqueue(c);
            return DataState.Instance;
        }
    }
}
