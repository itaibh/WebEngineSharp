using System;
using WebEngineSharp.DOM;

namespace WebEngineSharp.Tokenizer.InsertionMode
{
    // 8.2.5.4.6 http://www.w3.org/TR/html5/syntax.html#the-after-head-insertion-mode
    class AfterHeadInsertionModeState :BaseInsertionModeState
    {
        #region singleton

        private AfterHeadInsertionModeState()
        {
        }

        private static AfterHeadInsertionModeState s_Instance = new AfterHeadInsertionModeState();

        public static AfterHeadInsertionModeState Instance
        {
            get{ return s_Instance; }
        }

        #endregion

        public override BaseInsertionModeState ProcessToken(HtmlTokenizer tokenizer, ITokenQueue queue, BaseToken token, IDocument doc)
        {
            if (IsWhitespace(token))
            {
                InsertCharacter((CharacterToken)token, doc);
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
                switch(startTagToken.TagName)
                {
                    case "html":
                        //TODO - Process the token using the rules for the "in body" insertion mode.
                        break;
                    
                    case "body":
                        // TODO - Insert an HTML element for the token.
                        // TODO - Set the frameset-ok flag to "not ok".
                        return InBodyInsertionModeState.Instance;

                    case "frameset":
                        // TODO - Insert an HTML element for the token.
                        return InFramesetInsertionModeState.Instance;

                    case "base":
                    case "basefont":
                    case "bgsound":
                    case "link":
                    case "meta":
                    case "noframes":
                    case "script":
                    case "style":
                    case "template":
                    case "title":
                        ReportParseError();
                        // TODO - Push the node pointed to by the head element pointer onto the stack of open elements.
                        // TODO - Process the token using the rules for the "in head" insertion mode.
                        // TODO - Remove the node pointed to by the head element pointer from the stack of open elements. (It might not be the current node at this point.)
                        // TODO - NOTE: The head element pointer cannot be null at this point.
                        break;

                    case "head":
                        ReportParseError();
                        return this;
                }
            }
            EndTagToken endTagToken = token as EndTagToken;
            if (endTagToken != null)
            {
                switch (endTagToken.TagName)
                {
                    case "template":
                        // TODO - Process the token using the rules for the "in head" insertion mode.
                        break;

                    case "body":
                    case "html":
                    case "br":
                        // TODO - Act as described in the "anything else" entry below.
                        break;

                    default:
                        ReportParseError();
                        return this;
                }
            }

            StartTagToken dummyToken = new StartTagToken(){ TagName = "body" };
            InsertHtmlElement(dummyToken, doc);

            queue.EnqueueTokenForReprocessing(token);
            return InBodyInsertionModeState.Instance;
        }
    }
}
