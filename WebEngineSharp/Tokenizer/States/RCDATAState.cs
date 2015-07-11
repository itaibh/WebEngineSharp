using System;
using System.IO;

namespace WebEngineSharp.Tokenizer.States
{
    //8.2.4.3 http://www.w3.org/TR/html5/syntax.html#rcdata-state
    public class RCDATAState: BaseState
    {
        #region singleton

        private RCDATAState()
        {
        }

        private static RCDATAState s_Instance = new RCDATAState();

        public static RCDATAState Instance
        {
            get{ return s_Instance; }
        }

        #endregion

        public override BaseState Process(HtmlTokenizer tokenizer, StreamReader reader)
        {
            int c = Read(reader);
            switch (c)
            {
                case '&':
                    return CharacterReferenceInRCDATAState.Instance;

                case '<':
                    return RCDATALessThanSignState.Instance;

                case 0:
                    ReportParseError();
                    tokenizer.EmitChar('\uFFFD');
                    return this;

                case -1:
                    tokenizer.EmitToken(new EndOfFileToken());
                    return this;

                default:
                    tokenizer.EmitChar((char)c);
                    return this;
            }
        }
    }
}
