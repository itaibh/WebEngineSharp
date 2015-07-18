using System;
using WebEngineSharp.DOM;

namespace WebEngineSharp.Tokenizer.InsertionMode
{
    //8.2.5.4.22 http://www.w3.org/TR/html5/syntax.html#the-after-after-body-insertion-mode
    class AfterAfterBodyInsertionModeState : BaseInsertionModeState
    {
        #region singleton

        private AfterAfterBodyInsertionModeState()
        {
        }

        private static AfterAfterBodyInsertionModeState s_Instance = new AfterAfterBodyInsertionModeState();

        public static AfterAfterBodyInsertionModeState Instance
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
