using System;

namespace WebEngineSharp.Tokenizer.States
{
    //8.2.4.7 http://www.w3.org/TR/html5/syntax.html#plaintext-state
    public class PLAINTEXTState :BaseState
    {
        #region singleton

        private PLAINTEXTState()
        {
        }

        private static PLAINTEXTState s_Instance = new PLAINTEXTState();

        public static PLAINTEXTState Instance
        {
            get{ return s_Instance; }
        }

        #endregion

        public override BaseState Process(HtmlTokenizer tokenizer, System.IO.StreamReader reader)
        {
            for (;;){
                int c = Read(reader);
                switch(c)
                {
                    case 0:
                        ReportParseError();
                        tokenizer.EmitChar('\uFFFD');
                        break;
                    case -1:
                        tokenizer.EmitToken(new EndOfFileToken());
                        break;
                    default:
                        tokenizer.EmitChar((char)c);
                        return this;
                }
            }
        }
    }
}

