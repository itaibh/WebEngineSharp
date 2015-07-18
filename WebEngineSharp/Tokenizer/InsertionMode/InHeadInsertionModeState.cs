using System;
using WebEngineSharp.DOM;
using WebEngineSharp.DOM.Impl;
using WebEngineSharp.Tokenizer.States;

namespace WebEngineSharp.Tokenizer.InsertionMode
{
    // 8.2.5.4.4 http://www.w3.org/TR/html5/syntax.html#parsing-main-inhead
    class InHeadInsertionModeState : BaseInsertionModeState
    {
        #region singleton

        private InHeadInsertionModeState()
        {
        }

        private static InHeadInsertionModeState s_Instance = new InHeadInsertionModeState();

        public static InHeadInsertionModeState Instance
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
                switch (startTagToken.TagName)
                {
                    case "html":
                        //TODO - Process the token using the rules for the "in body" insertion mode.
                        break;

                    case "base":
                    case "basefont":
                    case "bgsound":
                    case "link":
                        //TODO - Insert an HTML element for the token. Immediately pop the current node off the stack of open elements.
                        //TODO - Acknowledge the token's self-closing flag, if it is set.
                        break;

                    case "meta":
                        //TODO - Insert an HTML element for the token. Immediately pop the current node off the stack of open elements.
                        //TODO - Acknowledge the token's self-closing flag, if it is set.
                        //TODO - If the element has a charset attribute, and getting an encoding from its value results in a supported ASCII-compatible character encoding or a UTF-16 encoding, and the confidence is currently tentative, then change the encoding to the resulting encoding.
                        //TODO - Otherwise, if the element has an http-equiv attribute whose value is an ASCII case-insensitive match for the string "Content-Type", and the element has a content attribute, and applying the algorithm for extracting a character encoding from a meta element to that attribute's value returns a supported ASCII-compatible character encoding or a UTF-16 encoding, and the confidence is currently tentative, then change the encoding to the extracted encoding.
                        break;

                    case "title":
                        //Follow the generic RCDATA element parsing algorithm. (http://www.w3.org/TR/html5/syntax.html#generic-rcdata-element-parsing-algorithm)
                        //IHTMLTitleElement titleElement = (IHTMLTitleElement)HtmlElementFactory.Instance.CreateElement("title", doc);
                        InsertHtmlElement(startTagToken, doc);
                        //doc.appendChild(titleElement); // TODO - Follow insert HTML element algorithm (http://www.w3.org/TR/html5/syntax.html#insert-an-html-element)
                        tokenizer.SetNextState(RCDATAState.Instance);
                        TreeConstruction.Instance.SaveCurrentInsertionModeState();
                        return TextInsertionModeState.Instance;

                    case "noframes":
                    case "style":
                        //TODO - Follow the generic raw text element parsing algorithm.
                        break;

                    case "noscript":
                        //TODO - If scripting flag (http://www.w3.org/TR/html5/syntax.html#scripting-flag) is enabled:
                        //TODO -     Follow the generic raw text element parsing algorithm.
                        //TODO - Else, if the flag is disabled:
                        //TODO -     Insert an HTML element for the token.
                        //TODO -     return InHeadNoScriptInsertionModeState.Instance;
                        break;

                    case "script":
                        //TODO - Run these steps:
                        //TODO - 1. Let the adjusted insertion location be the appropriate place for inserting a node.
                        //TODO - 2. Create an element for the token in the HTML namespace, with the intended parent being the element in which the adjusted insertion location finds itself.
                        //TODO - 3. Mark the element as being "parser-inserted" and unset the element's "force-async" flag.
                        //TODO - 
                        //TODO -    NOTE: This ensures that, if the script is external, any document.write() calls in the script will execute in-line, instead of blowing the document away,
                        //TODO -          as would happen in most other cases. It also prevents the script from executing until the end tag is seen.
                        //TODO - 
                        //TODO - 4. If the parser was originally created for the HTML fragment parsing algorithm, then mark the script element as "already started". (fragment case)
                        //TODO - 5. Insert the newly created element at the adjusted insertion location.
                        //TODO - 6. Push the element onto the stack of open elements so that it is the new current node.
                        //TODO - 7. Switch the tokenizer to the script data state.
                        //TODO - 8. Let the original insertion mode be the current insertion mode.
                        //       9. Switch the insertion mode to "text".
                        return TextInsertionModeState.Instance;

                    case "template":
                        //TODO - Insert an HTML element for the token.
                        //TODO - Insert a marker at the end of the list of active formatting elements.
                        //TODO - Set the frameset-ok flag to "not ok".
                        //TODO - Switch the insertion mode to "in template".
                        //TODO - Push "in template" onto the stack of template insertion modes so that it is the new current template insertion mode.
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
                    case "head":
                        //TODO - Pop the current node (which will be the head element) off the stack of open elements.
                        //TODO - Switch the insertion mode to "after head".
                        break;

                    case "body":
                    case "html":
                    case "br":
                        // Act as described in the "anything else" entry below.
                        break;

                    case "template":
                        //TODO - If there is no template element on the stack of open elements, then this is a parse error; ignore the token.
                        //TODO - Otherwise, run these steps:
                        //TODO - 1. Generate implied end tags.
                        //TODO - 2. If the current node is not a template element, then this is a parse error.
                        //TODO - 3. Pop elements from the stack of open elements until a template element has been popped from the stack.
                        //TODO - 4. Clear the list of active formatting elements up to the last marker.
                        //TODO - 5. Pop the current template insertion mode off the stack of template insertion modes.
                        //TODO - 6. Reset the insertion mode appropriately.
                        break;
                }
            }

            // Anything else:
            // Pop the current node (which will be the head element) off the stack of open elements.
            // Switch the insertion mode to "after head".
            // Reprocess the token.
            TreeConstruction.Instance.StackOfOpenElements.Pop();
            queue.EnqueueTokenForReprocessing(token);
            return AfterHeadInsertionModeState.Instance;
        }
    }
    
}
