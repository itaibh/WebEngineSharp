using System;
using System.IO;

namespace WebEngineSharp.Tokenizer.States
{
    // 8.2.4.4 http://www.w3.org/TR/html5/syntax.html#character-reference-in-rcdata-state
    public class CharacterReferenceInRCDATAState : BaseState
    {
        #region singleton

        private CharacterReferenceInRCDATAState()
        {
        }

        private static CharacterReferenceInRCDATAState s_Instance = new CharacterReferenceInRCDATAState();

        public static CharacterReferenceInRCDATAState Instance
        {
            get{ return s_Instance; }
        }

        #endregion

        public override BaseState Process(HtmlTokenizer tokenizer, StreamReader reader)
        {
            throw new NotImplementedException();
        }
    }
}

