using System;
using WebEngineSharp.DOM;

namespace WebEngineSharp.Tokenizer.InsertionMode
{

    class AfterFramesetInsertionModeState :BaseInsertionModeState
    {
        public override BaseInsertionModeState ProcessToken(HtmlTokenizer tokenizer, ITokenQueue queue, BaseToken token, IDocument doc)
        {
            throw new NotImplementedException();
        }
    }
    
}
