using System;
using WebEngineSharp.DOM;

namespace WebEngineSharp.Tokenizer.InsertionMode
{

    class AfterAfterFramesetInsertionModeState :BaseInsertionModeState
    {
        public override BaseInsertionModeState ProcessToken(HtmlTokenizer tokenizer, ITokenQueue queue, BaseToken token, IDocument doc)
        {
            throw new NotImplementedException();
        }
    }
}
