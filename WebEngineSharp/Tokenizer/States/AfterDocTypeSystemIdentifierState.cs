using System;
using System.Collections.Specialized;
using System.IO;

namespace WebEngineSharp.Tokenizer.States
{
    // 8.2.4.66 http://www.w3.org/TR/html5/syntax.html#after-doctype-system-identifier-state
    public class AfterDocTypeSystemIdentifierState: BaseState
    {
        private AfterDocTypeSystemIdentifierState()
        {
        }

        private static AfterDocTypeSystemIdentifierState s_Instance = new AfterDocTypeSystemIdentifierState();

        public static AfterDocTypeSystemIdentifierState Instance
        {
            get { return s_Instance; }
        }

        public DocTypeToken Token { get; set; }

        public override BaseState Process(HtmlTokenizer tokenizer, StreamReader reader)
        {
            int c;
            do
            {
                c = Read(reader);
            } while (IsWhitespace(c));

            if (c == '>')
            {
                tokenizer.EmitToken(Token);
                return DataState.Instance;
            }

            if (c == -1)
            {
                ReportParseError();
                Token.ForceQuirks = true;
                tokenizer.EmitToken(Token);
                return DataState.Instance;
                // Reconsume the EOF character (?)
            }

            ReportParseError();
            return BogusDocTypeState.Instance;
        }
    }


}
