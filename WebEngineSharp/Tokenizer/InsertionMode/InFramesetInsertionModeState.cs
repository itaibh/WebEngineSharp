using System;
using WebEngineSharp.DOM;

namespace WebEngineSharp.Tokenizer.InsertionMode
{

    class InFramesetInsertionModeState :BaseInsertionModeState
    {
        #region singleton

        private InFramesetInsertionModeState()
        {
        }

        private static InFramesetInsertionModeState s_Instance = new InFramesetInsertionModeState();

        public static InFramesetInsertionModeState Instance
        {
            get{ return s_Instance; }
        }

        #endregion
        public override BaseInsertionModeState ProcessToken(HtmlTokenizer tokenizer, ITokenQueue queue, BaseToken token, IDocument doc)
        {
            throw new NotImplementedException();
        }
    }
    
}
