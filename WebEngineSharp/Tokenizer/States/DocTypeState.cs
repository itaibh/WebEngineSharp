using System;
using System.Collections.Specialized;
using System.IO;

namespace WebEngineSharp.Tokenizer.States
{
    // 8.2.4.52 http://www.w3.org/TR/html5/syntax.html#doctype-state
    public class DocTypeState : BaseState
    {
        private DocTypeState()
        {
        }

        private static DocTypeState s_Instance = new DocTypeState();

        public static DocTypeState Instance
        {
            get { return s_Instance; }
        }

        public override BaseState Process(HtmlTokenizer tokenizer, StreamReader reader)
        {
            int c = Read(reader);
            if (IsWhitespace(c))
            {
                return BeforeDocTypeNameState.Instance;
            } 

            if (c==-1)
            {
                ReportParseError();
                DocTypeToken token = new DocTypeToken() { ForceQuirks = true };
                return DataState.Instance;
                // Parse error. Switch to the data state. Reconsume the EOF character.
            }

            ReportParseError();
            BeforeDocTypeNameState.Instance.LastConsumedCharacters.Enqueue((char)c);
            return BeforeDocTypeNameState.Instance;
        }
    }
}
