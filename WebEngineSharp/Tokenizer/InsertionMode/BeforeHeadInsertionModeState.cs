using System;
using WebEngineSharp.DOM;
using WebEngineSharp.DOM.Impl;

namespace WebEngineSharp.Tokenizer.InsertionMode
{
    // 8.2.5.4.3 http://www.w3.org/TR/html5/syntax.html#the-before-head-insertion-mode
    class BeforeHeadInsertionModeState : BaseInsertionModeState
    {
        #region singleton
        private static BeforeHeadInsertionModeState s_Instance = new BeforeHeadInsertionModeState();

        private BeforeHeadInsertionModeState()
        {
        }

        public static BeforeHeadInsertionModeState Instance
        {
            get { return s_Instance; }
        }
        #endregion

        public override BaseInsertionModeState ProcessToken(HtmlTokenizer tokenizer, ITokenQueue queue, BaseToken token, IDocument doc)
        {
            if (IsWhitespace(token))
            {
                return this;
            }

            CommentToken commentToken = token as CommentToken;
            if (commentToken != null)
            {
                InsertComment(commentToken, doc);
                return this;
            }

            if (token is DocTypeToken)
            {
                ReportParseError();
                return this;
            }

            StartTagToken startTagToken = token as StartTagToken;
            if (startTagToken != null)
            {
                if (startTagToken.TagName == "html")
                {
                    //TODO - Process the token using the rules for the "in body" insertion mode.
                }
                else if (startTagToken.TagName == "head")
                {
                    ((Document)doc).head = (IHTMLHeadElement)base.InsertHtmlElement(startTagToken, doc);
                    return InHeadInsertionModeState.Instance;
                }
            }

            EndTagToken endTagToken = token as EndTagToken;
            if ((endTagToken == null) || 
                (endTagToken != null &&
                 endTagToken.TagName != "head" &&
                 endTagToken.TagName != "body" &&
                 endTagToken.TagName != "html" &&
                 endTagToken.TagName != "br"))
            {
                ReportParseError();
                return this;
            }

            //Insert an HTML element for a "head" start tag token with no attributes.
            //Set the head element pointer to the newly created head element.
            StartTagToken dummyToken = new StartTagToken(){ TagName = "head" };
            ((Document)doc).head = (IHTMLHeadElement)InsertHtmlElement(dummyToken, doc);

            //Switch the insertion mode to "in head".
            //Reprocess the current token.
            queue.EnqueueTokenForReprocessing(token);
            return InHeadInsertionModeState.Instance;
        }
    }
}
