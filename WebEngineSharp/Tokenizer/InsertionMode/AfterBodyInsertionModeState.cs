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
            if (IsWhitespace(token))
            {
                return ProcessUsingRulesOf(tokenizer, queue, token, doc);
            }

            CommentToken comment = token as CommentToken;
            if (comment != null)
            {
                //TODO - Insert a comment as the last child of the first element in the stack of open elements (the html element).
                return this;
            }

            if (token is DocTypeToken)
            {
                ReportParseError();
                return this;
            }

            StartTagToken startToken = token as StartTagToken;
            if (startToken != null && startToken.TagName == "html")
            {
                return ProcessUsingRulesOf(tokenizer, queue, token, doc);
            }

            EndTagToken endToken = token as EndTagToken;
            if (endToken != null && endToken.TagName == "html")
            {
                //TODO - If the parser was originally created as part of the HTML fragment parsing algorithm,
                //TODO - this is a parse error; ignore the token. (fragment case)
                //TODO - Otherwise, switch the insertion mode to "after after body".
                return AfterAfterBodyInsertionModeState.Instance;
            }

            if (token is EndOfFileToken)
            {
                base.StopParsing();
                return null;
            }

            ReportParseError();
            queue.EnqueueTokenForReprocessing(token);
            return InBodyInsertionModeState.Instance;
        }

        private BaseInsertionModeState ProcessUsingRulesOf(HtmlTokenizer tokenizer, ITokenQueue queue, BaseToken token, IDocument doc)
        {
            BaseInsertionModeState nextState = InBodyInsertionModeState.Instance.ProcessToken(tokenizer, queue, token, doc);
            if (nextState != InBodyInsertionModeState.Instance)
            {
                return nextState;
            }
            return this;
        }
    }
}
