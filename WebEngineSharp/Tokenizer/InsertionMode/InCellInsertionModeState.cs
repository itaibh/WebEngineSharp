using System;
using WebEngineSharp.DOM;

namespace WebEngineSharp.Tokenizer.InsertionMode
{
    //8.2.5.4.15 http://www.w3.org/TR/html5/syntax.html#parsing-main-intd
    class InCellInsertionModeState :BaseInsertionModeState
    {
        #region singleton

        private InCellInsertionModeState()
        {
        }

        private static InCellInsertionModeState s_Instance = new InCellInsertionModeState();

        public static InCellInsertionModeState Instance
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
