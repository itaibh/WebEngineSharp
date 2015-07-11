using System;
using WebEngineSharp.DOM;
using WebEngineSharp.DOM.Impl;

namespace WebEngineSharp.Tokenizer.InsertionMode
{
    //8.2.5.4.2 http://www.w3.org/TR/html5/syntax.html#the-before-html-insertion-mode
    class BeforeHTMLInsertionModeState : BaseInsertionModeState
    {
        private static BeforeHTMLInsertionModeState s_Instance = new BeforeHTMLInsertionModeState();

        private BeforeHTMLInsertionModeState()
        {
        }

        public static BeforeHTMLInsertionModeState Instance
        {
            get { return s_Instance; }
        }

        public override BaseInsertionModeState ProcessToken(HtmlTokenizer tokenizer, ITokenQueue queue, BaseToken token, IDocument doc)
        {
            if (token is DocTypeToken)
            {
                ReportParseError();
                return this;
            }

            CommentToken commentToken = token as CommentToken;
            if (commentToken != null)
            {
                IComment commentNode = new Comment(doc, commentToken.Comment);
                doc.appendChild(commentNode);
                return this;
            }

            if (IsWhitespace(token))
            {
                return this;
            }


            // * A start tag whose tag name is "html":
            StartTagToken startTagToken = token as StartTagToken;
            if (startTagToken != null && startTagToken.TagName == "html")
            {

                //   Create an element for the token (http://www.w3.org/TR/html5/syntax.html#create-an-element-for-the-token)
                //   in the HTML namespace, with the Document as the intended parent.
                //   Append it to the Document object. Put this element in the stack of open elements.
                //    
                //   If the Document is being loaded as part of navigation of a browsing context,
                //   then: if the newly created element has a manifest attribute whose value is not the empty string,
                //   then resolve the value of that attribute to an absolute URL, relative to the newly created element,
                //   and if that is successful, run the application cache selection algorithm with the result of applying
                //   the URL serializer algorithm to the resulting parsed URL with the exclude fragment flag set; otherwise,
                //   if there is no such attribute, or its value is the empty string, or resolving its value fails, run the
                //   application cache selection algorithm with no manifest. The algorithm must be passed the Document object.

                // If the Document is being loaded as part of navigation of a browsing context:
                // RunApplicationCacheSelectionAlgorithm();
            }

            // * An end tag whose tag name is one of: "head", "body", "html", "br"
            //   Act as described in the "anything else" entry below.
            //
            // * Any other end tag
            //   Parse error. Ignore the token.
            EndTagToken endTagToken = token as EndTagToken;
            if (endTagToken != null &&
                endTagToken.TagName != "head" &&
                endTagToken.TagName != "body" &&
                endTagToken.TagName != "html" &&
                endTagToken.TagName != "br")
            {
                ReportParseError();
                return this;
            }

            // * Anything else
            //   Create an html element whose ownerDocument is the Document object. Append it to the Document object.
            //   Put this element in the stack of open elements.
            // 
            //   If the Document is being loaded as part of navigation of a browsing context,
            //   then: run the application cache selection algorithm with no manifest, passing it the Document object.
            // 
            //   Switch the insertion mode to "before head", then reprocess the token.
            // 
            // The root element can end up being removed from the Document object, e.g. by scripts; nothing in particular
            // happens in such cases, content continues being appended to the nodes as described in the next section.

            HTMLElement htmlElement = new HTMLElement(doc, ((TagToken)token).TagName);
            doc.appendChild(htmlElement);
            TreeConstruction.Instance.StackOfOpenElements.Push(htmlElement);

            // If the Document is being loaded as part of navigation of a browsing context:
            // RunApplicationCacheSelectionAlgorithm();

            return BeforeHeadInsertionModeState.Instance;
        }

        // 5.7.5 http://www.w3.org/TR/html5/browsers.html#the-application-cache-selection-algorithm
        private void RunApplicationCacheSelectionAlgorithm()
        {
        }
    }
}
