using System;
using System.Collections.Specialized;
using System.IO;

namespace WebEngineSharp.Tokenizer.States
{
    // 8.2.4.1 http://www.w3.org/TR/html5/syntax.html#data-state
    public class DataState : BaseState
    {
        private DataState()
        {
        }

        private static DataState s_Instance = new DataState();

        public static DataState Instance
        {
            get { return s_Instance; }
        }

        public override BaseState Process(HtmlTokenizer tokenizer, StreamReader reader)
        {
            for (;;)
            {
                int c = Read(reader);
                switch (c)
                {
                    case '&':
                        CharacterReferenceInDataState.Instance.Process(tokenizer, reader, null);
                        break;

                    case '<':
                        return TagOpenState.Instance;

                    case '\0':
                        ReportParseError();
                        tokenizer.EmitChar('\0');
                        break;

                    case -1:
                        tokenizer.EmitToken(new EndOfFileToken());
                        return null;

                    default:
                        tokenizer.EmitChar((char)c);
                        return this; // Required to allow switching the state.
                }
            }
        }
    }
}
