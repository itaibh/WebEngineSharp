using System;
using WebEngineSharp.DOM;

namespace WebEngineSharp.Tokenizer.InsertionMode
{
    // 8.2.5.4.7 http://www.w3.org/TR/html5/syntax.html#parsing-main-inbody
    class InBodyInsertionModeState :BaseInsertionModeState
    {
        #region singleton

        private InBodyInsertionModeState()
        {
        }

        private static InBodyInsertionModeState s_Instance = new InBodyInsertionModeState();

        public static InBodyInsertionModeState Instance
        {
            get{ return s_Instance; }
        }

        #endregion

        public override BaseInsertionModeState ProcessToken(HtmlTokenizer tokenizer, ITokenQueue queue, BaseToken token, IDocument doc)
        {
            //TODO - PERFORMANCE!
            CharacterToken charToken = token as CharacterToken;
            if (charToken != null)
            {
                if (charToken.Character == 0)
                {
                    ReportParseError();
                    return this;
                }

                if (IsWhitespace(charToken))
                {
                    //TODO - Reconstruct the active formatting elements, if any. (http://www.w3.org/TR/html5/syntax.html#reconstruct-the-active-formatting-elements)
                    //       Insert the token's character. (http://www.w3.org/TR/html5/syntax.html#insert-a-character)
                    InsertCharacter(charToken, doc);
                    return this;
                }

                //TODO - Reconstruct the active formatting elements, if any. (http://www.w3.org/TR/html5/syntax.html#reconstruct-the-active-formatting-elements)
                //       Insert the token's character. (http://www.w3.org/TR/html5/syntax.html#insert-a-character)
                InsertCharacter(charToken, doc);
                //TODO - Set the frameset-ok flag to "not ok". (http://www.w3.org/TR/html5/syntax.html#frameset-ok-flag)
                return this;
            }

            CommentToken comment = token as CommentToken;
            if (comment != null)
            {
                InsertComment(comment, doc);
                return this;
            }

            if (token is DocTypeToken)
            {
                ReportParseError();
                return this;
            }

            StartTagToken startTag = token as StartTagToken;
            EndTagToken endTag = token as EndTagToken;

            if (startTag != null && startTag.TagName == "html")
            {
                ReportParseError();
                //TODO - If there is a template element on the stack of open elements, then ignore the token.
                //TODO - Otherwise, for each attribute on the token, check to see if the attribute is already
                //TODO - present on the top element of the stack of open elements. If it is not, add the attribute
                //TODO - and its corresponding value to that element.
                return this;
            }

            if ((startTag != null && (
                    startTag.TagName == "base" ||
                    startTag.TagName == "basefont" ||
                    startTag.TagName == "bgsound" ||
                    startTag.TagName == "link" ||
                    startTag.TagName == "meta" ||
                    startTag.TagName == "noframes" ||
                    startTag.TagName == "script" ||
                    startTag.TagName == "style" ||
                    startTag.TagName == "template" ||
                    startTag.TagName == "title")) ||
                (endTag != null && endTag.TagName == "template"))
            {
                BaseInsertionModeState nextState = InHeadInsertionModeState.Instance.ProcessToken(tokenizer, queue, token, doc);
                if (nextState != InHeadInsertionModeState.Instance)
                {
                    return nextState;
                }
                return this;
            }

            if (startTag != null && startTag.TagName == "body")
            {
                ReportParseError();
                //TODO - If the second element on the stack of open elements is not a body element, if the stack of open elements
                //TODO - has only one node on it, or if there is a template element on the stack of open elements, then ignore the
                //TODO - token. (fragment case)
                //TODO - Otherwise, set the frameset-ok flag to "not ok"; then, for each attribute on the token, check to see if
                //TODO - the attribute is already present on the body element (the second element) on the stack of open elements,
                //TODO - and if it is not, add the attribute and its corresponding value to that element.
                return this;
            }

            if (startTag != null && startTag.TagName == "frameset")
            {
                ReportParseError();
                //TODO - If the stack of open elements has only one node on it, or if the second element on the stack of open
                //TODO - elements is not a body element, then ignore the token. (fragment case)

                //TODO - If the frameset-ok flag is set to "not ok", ignore the token.
                //TODO - Otherwise, run the following steps:
                //TODO -     1. Remove the second element on the stack of open elements from its parent node, if it has one.
                //TODO -     2. Pop all the nodes from the bottom of the stack of open elements, from the current node up to,
                //TODO -        but not including, the root html element.
                //TODO -     3. Insert an HTML element for the token.
                //TODO -     4. Switch the insertion mode to "in frameset".
                return this;
            }

            if (token is EndOfFileToken)
            {
                //TODO - If there is a node in the stack of open elements that is not either a dd element, a dt element,
                //TODO - an li element, a p element, a tbody element, a td element, a tfoot element, a th element,
                //TODO - a thead element, a tr element, the body element, or the html element, then this is a parse error.

                //TODO - If the stack of template insertion modes is not empty, then process the token using the rules for
                //TODO - the "in template" insertion mode.

                //       Otherwise, stop parsing.
                base.StopParsing();
                return null;
            }

            if (endTag != null && endTag.TagName == "body")
            {
                return ProcessEndTagBodyOrHtml();
            }

            if (endTag != null && endTag.TagName == "html")
            {
                BaseInsertionModeState nextState = ProcessEndTagBodyOrHtml();
                queue.EnqueueTokenForReprocessing(token);
                return nextState;
            }

            if (startTag != null && (
                    startTag.TagName == "address" ||
                    startTag.TagName == "article" ||
                    startTag.TagName == "aside" ||
                    startTag.TagName == "blockquote" ||
                    startTag.TagName == "center" ||
                    startTag.TagName == "details" ||
                    startTag.TagName == "dialog" ||
                    startTag.TagName == "dir" ||
                    startTag.TagName == "div" ||
                    startTag.TagName == "dl" ||
                    startTag.TagName == "fieldset" ||
                    startTag.TagName == "figcaption" ||
                    startTag.TagName == "figure" ||
                    startTag.TagName == "footer" ||
                    startTag.TagName == "header" ||
                    startTag.TagName == "hgroup" ||
                    startTag.TagName == "main" ||
                    startTag.TagName == "nav" ||
                    startTag.TagName == "ol" ||
                    startTag.TagName == "p" ||
                    startTag.TagName == "section" ||
                    startTag.TagName == "summary" ||
                    startTag.TagName == "ul"))
            {
                //TODO - If the stack of open elements has a p element in button scope, then close a p element. (http://www.w3.org/TR/html5/syntax.html#close-a-p-element)
                //       Insert an HTML element for the token.
                InsertHtmlElement(startTag, doc);
                return this;
            }

            if (startTag != null && (
                    startTag.TagName == "h1" ||
                    startTag.TagName == "h2" ||
                    startTag.TagName == "h3" ||
                    startTag.TagName == "h4" ||
                    startTag.TagName == "h5" ||
                    startTag.TagName == "h6"))
            {
                //TODO - If the stack of open elements has a p element in button scope, then close a p element. (http://www.w3.org/TR/html5/syntax.html#close-a-p-element)
                //TODO - If the current node is an HTML element whose tag name is one of "h1", "h2", "h3", "h4", "h5", or "h6",
                //TODO - then this is a parse error; pop the current node off the stack of open elements.
                InsertHtmlElement(startTag, doc);
                return this;
            }

            if (startTag != null && (
                    startTag.TagName == "pre" ||
                    startTag.TagName == "listing"))
            {
                //TODO
                return this;
            }

            if (startTag != null && startTag.TagName == "form")
            {
                //TODO
                return this;
            }

            if (startTag != null && startTag.TagName == "li")
            {
                //TODO
                return this;
            }

            if (startTag != null && (
                    startTag.TagName == "dd" ||
                    startTag.TagName == "dt"))
            {
                //TODO
                return this;
            }

            if (startTag != null && startTag.TagName == "plaintext")
            {
                //TODO - If the stack of open elements has a p element in button scope, then close a p element. (http://www.w3.org/TR/html5/syntax.html#close-a-p-element)
                //       Insert an HTML element for the token.
                InsertHtmlElement(startTag, doc);
                //       Switch the tokenizer to the PLAINTEXT state.
                tokenizer.SetNextState(WebEngineSharp.Tokenizer.States.PLAINTEXTState.Instance);
                //TODO - NOTE: Once a start tag with the tag name "plaintext" has been seen, that will be the last
                //TODO - token ever seen other than character tokens (and the end-of-file token), because there is
                //TODO - no way to switch out of the PLAINTEXT state.
                return this;
            }

            if (startTag != null && startTag.TagName == "button")
            {
                //TODO
                return this;
            }

            if (endTag != null && (
                    endTag.TagName == "address" ||
                    endTag.TagName == "article" ||
                    endTag.TagName == "aside" ||
                    endTag.TagName == "blockquote" ||
                    endTag.TagName == "button" ||
                    endTag.TagName == "center" ||
                    endTag.TagName == "details" ||
                    endTag.TagName == "dialog" ||
                    endTag.TagName == "dir" ||
                    endTag.TagName == "div" ||
                    endTag.TagName == "dl" ||
                    endTag.TagName == "fieldset" ||
                    endTag.TagName == "figcaption" ||
                    endTag.TagName == "figure" ||
                    endTag.TagName == "footer" ||
                    endTag.TagName == "header" ||
                    endTag.TagName == "hgroup" ||
                    endTag.TagName == "listing" ||
                    endTag.TagName == "main" ||
                    endTag.TagName == "nav" ||
                    endTag.TagName == "ol" ||
                    endTag.TagName == "pre" ||
                    endTag.TagName == "section" ||
                    endTag.TagName == "summary" ||
                    endTag.TagName == "ul"))
            {
                //TODO
                return this;
            }

            if (endTag != null && endTag.TagName == "form")
            {
                //TODO
                return this;
            }

            if (endTag != null && endTag.TagName == "p")
            {
                //TODO
                return this;
            }

            if (endTag != null && endTag.TagName == "li")
            {
                //TODO
                return this;
            }

            if (endTag != null && (
                    endTag.TagName == "dd" ||
                    endTag.TagName == "dt"))
            {
                //TODO
                return this;
            }

            if (endTag != null && (
                    endTag.TagName == "h1" ||
                    endTag.TagName == "h2" ||
                    endTag.TagName == "h3" ||
                    endTag.TagName == "h4" ||
                    endTag.TagName == "h5" ||
                    endTag.TagName == "h6"))
            {
                //TODO
                return this;
            }

            if (endTag != null && endTag.TagName == "sarcasm")
            {
                //TODO - Take a deep breath, then act as described in the "any other end tag" entry below.
                return this;
            }

            if (startTag != null && startTag.TagName == "a")
            {
                //TODO
                return this;
            }

            if (startTag != null && (
                    startTag.TagName == "b" ||
                    startTag.TagName == "big" ||
                    startTag.TagName == "code" ||
                    startTag.TagName == "em" ||
                    startTag.TagName == "font" ||
                    startTag.TagName == "i" ||
                    startTag.TagName == "s" ||
                    startTag.TagName == "small" ||
                    startTag.TagName == "strike" ||
                    startTag.TagName == "strong" ||
                    startTag.TagName == "tt" ||
                    startTag.TagName == "u"))
            {
                //TODO
                return this;
            }

            if (startTag != null && startTag.TagName == "nobr")
            {
                //TODO
                return this;
            }

            if (endTag != null && (
                    endTag.TagName == "a" ||
                    endTag.TagName == "b" ||
                    endTag.TagName == "big" ||
                    endTag.TagName == "code" ||
                    endTag.TagName == "em" ||
                    endTag.TagName == "font" ||
                    endTag.TagName == "i" ||
                    endTag.TagName == "nobr" ||
                    endTag.TagName == "s" ||
                    endTag.TagName == "small" ||
                    endTag.TagName == "strike" ||
                    endTag.TagName == "strong" ||
                    endTag.TagName == "tt" ||
                    endTag.TagName == "u"))
            {
                //TODO - Run the adoption agency algorithm (http://www.w3.org/TR/html5/syntax.html#adoption-agency-algorithm) for the token's tag name.
                return this;
            }


            if (startTag != null && (
                    startTag.TagName == "applet" ||
                    startTag.TagName == "marquee" ||
                    startTag.TagName == "object"))
            {
                //TODO
                return this;
            }

            if (endTag != null && (
                    endTag.TagName == "applet" ||
                    endTag.TagName == "marquee" ||
                    endTag.TagName == "object"))
            {
                //TODO
                return this;
            }

            if (startTag != null && startTag.TagName == "table")
            {
                //TODO
                return this;
            }

            if (endTag != null && endTag.TagName == "br")
            {
                //TODO
                return this;
            }

            if (startTag != null && (
                    startTag.TagName == "area" ||
                    startTag.TagName == "br" ||
                    startTag.TagName == "embed" ||
                    startTag.TagName == "img" ||
                    startTag.TagName == "keygen" ||
                    startTag.TagName == "wbr"))
            {
                //TODO
                return this;
            }

            if (startTag != null && startTag.TagName == "input")
            {
                //TODO
                return this;
            }

            if (startTag != null && (
                    startTag.TagName == "param" ||
                    startTag.TagName == "source" ||
                    startTag.TagName == "track"))
            {
                //TODO
                return this;
            }

            if (startTag != null && startTag.TagName == "hr")
            {
                //TODO
                return this;
            }

            if (startTag != null && startTag.TagName == "image")
            {
                //Parse error. Change the token's tag name to "img" and reprocess it. (Don't ask.)
                ReportParseError();
                startTag.TagName = "img";
                queue.EnqueueTokenForReprocessing(startTag);
                return this;
            }

            if (startTag != null && startTag.TagName == "isindex")
            {
                //TODO
                return this;
            }

            if (startTag != null && startTag.TagName == "textarea")
            {
                //TODO
                return this;
            }

            if (startTag != null && startTag.TagName == "xmp")
            {
                //TODO
                return this;
            }

            if (startTag != null && startTag.TagName == "iframe")
            {
                //TODO - Set the frameset-ok flag (http://www.w3.org/TR/html5/syntax.html#frameset-ok-flag) to "not ok".
                //TODO - Follow the generic raw text element parsing algorithm. (http://www.w3.org/TR/html5/syntax.html#generic-raw-text-element-parsing-algorithm)
                return this;
            }


            if (startTag != null &&
                (startTag.TagName == "noembed" ||
                (startTag.TagName == "noscript" && tokenizer.ScriptingEnabled)))
            {
                //TODO - Follow the generic raw text element parsing algorithm. (http://www.w3.org/TR/html5/syntax.html#generic-raw-text-element-parsing-algorithm)
                return this;
            }

            if (startTag != null && startTag.TagName == "select")
            {
                //TODO
                return this;
            }

            if (startTag != null && (
                    startTag.TagName == "optgroup" ||
                    startTag.TagName == "option"))
            {
                //TODO
                return this;
            }

            if (startTag != null && (
                    startTag.TagName == "rb" ||
                    startTag.TagName == "rp" ||
                    startTag.TagName == "rtc"))
            {
                //TODO
                return this;
            }

            if (startTag != null && startTag.TagName == "rt")
            {
                //TODO
                return this;
            }

            if (startTag != null && startTag.TagName == "math")
            {
                //TODO
                return this;
            }

            if (startTag != null && startTag.TagName == "svg")
            {
                //TODO
                return this;
            }

            if (startTag != null && (
                    startTag.TagName == "caption" ||
                    startTag.TagName == "col" ||
                    startTag.TagName == "colgroup" ||
                    startTag.TagName == "frame" ||
                    startTag.TagName == "head" ||
                    startTag.TagName == "tbody" ||
                    startTag.TagName == "td" ||
                    startTag.TagName == "tfoot" ||
                    startTag.TagName == "th" ||
                    startTag.TagName == "thead" ||
                    startTag.TagName == "tr"))
            {
                ReportParseError();
                return this;
            }

            if (startTag != null)
            {
                //TODO - Reconstruct the active formatting elements, if any.
                //       Insert an HTML element for the token.
                InsertHtmlElement(startTag, doc);
                //NOTE: This element will be an ordinary element.
                return this;
            }

            if (endTag != null)
            {
                //TODO
                return this;
            }

            return this;
        }

        private BaseInsertionModeState ProcessEndTagBodyOrHtml()
        {
            //TODO - If the stack of open elements does not have a body element in scope, this is a parse error; ignore the token.
            //TODO - Otherwise, if there is a node in the stack of open elements that is not either a dd element, a dt element, an li element, an optgroup element, an option element, a p element, an rb element, an rp element, an rt element, an rtc element, a tbody element, a td element, a tfoot element, a th element, a thead element, a tr element, the body element, or the html element, then this is a parse error.
            return AfterBodyInsertionModeState.Instance;
        }

        private void ClosePElement()
        {
            //TODO - 1. Generate implied end tags, except for p elements.
            //TODO - 2. If the current node is not a p element, then this is a parse error.
            //TODO - 3. Pop elements from the stack of open elements until a p element has been popped from the stack.
        }
    }
    
}
