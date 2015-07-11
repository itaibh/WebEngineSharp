using System;
using System.IO;

namespace WebEngineSharp.Tokenizer.States
{
    //8.2.4.11 http://www.w3.org/TR/html5/syntax.html#rcdata-less-than-sign-state
    public class RCDATALessThanSignState : BaseState
	{
        #region singleton

        private RCDATALessThanSignState()
        {
        }

        private static RCDATALessThanSignState s_Instance = new RCDATALessThanSignState();

        public static RCDATALessThanSignState Instance
        {
            get{ return s_Instance; }
        }

        #endregion

        public override BaseState Process(HtmlTokenizer tokenizer, StreamReader reader)
        {
            int c = Read(reader);
            if (c == '/')
            {
                tokenizer.TemporaryBuffer.Clear();
                return RCDATAEndTagOpenState.Instance;
            }
            tokenizer.EmitChar('<');
            LastConsumedCharacters.Enqueue((char)c);
            return RCDATAState.Instance;
        }
	}

}

