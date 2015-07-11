using System;
using System.Collections.Specialized;
using System.IO;

namespace WebEngineSharp.Tokenizer.States
{
    // 8.2.4.45 http://www.w3.org/TR/html5/syntax.html#markup-declaration-open-state
    public class MarkupDeclarationOpenState : BaseState
    {
        private MarkupDeclarationOpenState()
        {
        }

        private static MarkupDeclarationOpenState s_Instance = new MarkupDeclarationOpenState();

        public static MarkupDeclarationOpenState Instance
        {
            get { return s_Instance; }
        }

        public override BaseState Process(HtmlTokenizer tokenizer, StreamReader reader)
        {
            char[] buffer = new char[7];
            int count = ReadBlock(reader, buffer, 0, 2);

            string tokenStr = new string(buffer, 0, 2);
            if (tokenStr == "--")
            {
                CommentToken token = new CommentToken();
                CommentStartState.Instance.Token = token;
                return CommentStartState.Instance;
            } 

            count = ReadBlock(reader, buffer, 2, 5);
            tokenStr = new string(buffer);

            if (tokenStr.Equals("doctype", StringComparison.OrdinalIgnoreCase))
            {
                return DocTypeState.Instance;
            } 

            if (tokenStr == "[CDATA[")
            {
                //TODO - if there is an adjusted current node and it is not an element in the HTML namespace 
                return CdataSectionState.Instance;
            }

            ReportParseError();
            BogusCommentState.Instance.Comment.Append(tokenStr);
            return BogusCommentState.Instance;

            // If the next two characters are both "-" (U+002D) characters, consume those two characters, create a
            // comment token whose data is the empty string, and switch to the comment start state.
            //
            // Otherwise, if the next seven characters are an ASCII case-insensitive match for the word "DOCTYPE",
            // then consume those characters and switch to the DOCTYPE state.
            //
            // Otherwise, if there is an adjusted current node and it is not an element in the HTML namespace and the
            // next seven characters are a case-sensitive match for the string "[CDATA[" (the five uppercase letters
            // "CDATA" with a U+005B LEFT SQUARE BRACKET character before and after), then consume those characters and
            // switch to the CDATA section state.
            //
            // Otherwise, this is a parse error. Switch to the bogus comment state. The next character that is consumed,
            // if any, is the first character that will be in the comment.
        }
    }
}
