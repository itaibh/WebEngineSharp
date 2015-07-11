using System;
using WebEngineSharp.DOM;

namespace WebEngineSharp.Tokenizer.InsertionMode
{
    //8.2.5.4.19 http://www.w3.org/TR/html5/syntax.html#parsing-main-afterbody
    class AfterBodyInsertionModeState :BaseInsertionModeState
    {
        #region singleton

        private AfterBodyInsertionModeState()
        {
        }

        private static AfterBodyInsertionModeState s_Instance = new AfterBodyInsertionModeState();

        public static AfterBodyInsertionModeState Instance
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
