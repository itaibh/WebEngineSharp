using System;
using System.Collections.Specialized;
using System.IO;

namespace WebEngineSharp.Tokenizer.States
{
    // 8.2.4.67 http://www.w3.org/TR/html5/syntax.html#bogus-doctype-state
    public class BogusDocTypeState : BaseState
    {
        private BogusDocTypeState()
        {
        }

        public DocTypeToken Token { get; set; }

        private static BogusDocTypeState s_Instance = new BogusDocTypeState();

        public static BogusDocTypeState Instance
        {
            get { return s_Instance; }
        }

        public override BaseState Process(HtmlTokenizer tokenizer, StreamReader reader)
        {
            for (;;)
            {
                int c = Read(reader);

                if (c == '>')
                {
                    tokenizer.EmitToken(Token);
                    return DataState.Instance;
                }

                if (c == -1)
                {
                    tokenizer.EmitToken(Token);
                    return DataState.Instance;
                    // Reconsume the EOF character. (?)
                }
            }
        }
    }
}
