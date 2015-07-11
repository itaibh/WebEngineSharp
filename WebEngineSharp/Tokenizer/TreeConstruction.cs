using System;
using System.Collections.Generic;
using WebEngineSharp.DOM;
using WebEngineSharp.DOM.Impl;
using WebEngineSharp.Tokenizer.InsertionMode;

namespace WebEngineSharp.Tokenizer
{
    // 8.2.5 http://www.w3.org/TR/html5/syntax.html#tree-construction
    public class TreeConstruction
    {
        #region singleton
        private TreeConstruction()
        {
            this.Document = new Document();
            m_InsertionModeTokenProcessor = new InsertionModeTokenProcessingQueue(Document);
            StackOfOpenElements = new Stack<INode>();
            StackOfOpenElements.Push(new HTMLHtmlElement(this.Document));
        }

        private static TreeConstruction s_Instance = new TreeConstruction();

        public static TreeConstruction Instance
        {
            get { return s_Instance; }
        }

        #endregion

        public IDocument Document { get; private set; }

        private InsertionModeTokenProcessingQueue m_InsertionModeTokenProcessor;

        // 8.2.3.2 http://www.w3.org/TR/html5/syntax.html#the-stack-of-open-elements
        public Stack<INode> StackOfOpenElements { get; private set; }

        public INode AdjustedCurrentNode
        {
            get {
                if (StackOfOpenElements.Count > 0)
                {
                    return StackOfOpenElements.Peek();
                }

                return null;
            }
        }

        public void ProcessToken(HtmlTokenizer tokenizer, BaseToken token)
        {
            INode node = AdjustedCurrentNode;
            if (node == null || IsNodeInNamespace(node, HTMLNamespace))
            {
                ActionA(tokenizer, token);
                return;
            }

            StartTagToken startTagToken = token as StartTagToken;
            if (IsMathMLIntegrationPoint(node))
            {
                if (startTagToken != null && startTagToken.TagName != "mglyph" && startTagToken.TagName != "malignmark")
                {
                    ActionA(tokenizer, token);
                    return;
                }

                if (token is CharacterToken)
                {
                    ActionA(tokenizer, token);
                    return;
                }
            }

            if (IsNodeInNamespace(node, MathMLNamespace) && node.nodeName == "annotation-xml")
            {
                if (startTagToken != null && startTagToken.TagName == "svg")
                {
                    ActionA(tokenizer, token);
                    return;
                }
            }

            if (IsHTMLIntegrationPoint(node) && ((startTagToken != null) || (token is CharacterToken)))
            {
                ActionA(tokenizer, token);
                return;
            }

            if (token is EndOfFileToken)
            {
                ActionA(tokenizer, token);
                return;
            }

            ActionB(tokenizer, token);
            /*
The next token is the token that is about to be processed by the tree construction dispatcher (even if the token is subsequently just ignored).

A node is a MathML text integration point if it is one of the following elements:

An mi element in the MathML namespace
An mo element in the MathML namespace
An mn element in the MathML namespace
An ms element in the MathML namespace
An mtext element in the MathML namespace
A node is an HTML integration point if it is one of the following elements:

An annotation-xml element in the MathML namespace whose start tag token had an attribute with the name "encoding" whose value was an ASCII case-insensitive match for the string "text/html"
An annotation-xml element in the MathML namespace whose start tag token had an attribute with the name "encoding" whose value was an ASCII case-insensitive match for the string "application/xhtml+xml"
A foreignObject element in the SVG namespace
A desc element in the SVG namespace
A title element in the SVG namespace
Not all of the tag names mentioned below are conformant tag names in this specification; many are included to handle legacy content. They still form part of the algorithm that implementations are required to implement to claim conformance.

The algorithm described below places no limit on the depth of the DOM tree generated, or on the length of tag names, attribute names, attribute values, Text nodes, etc. While implementors are encouraged to avoid arbitrary limits, it is recognized that practical concerns will likely force user agents to impose nesting depth constraints.
             */
        }

        private void ActionA(HtmlTokenizer tokenizer, BaseToken token)
        {
            // Process the token according to the rules given in the section corresponding to the current insertion 
            // mode in HTML content.
            // http://www.w3.org/TR/html5/syntax.html#insertion-mode

            m_InsertionModeTokenProcessor.EnqueueToken(token);
            m_InsertionModeTokenProcessor.ProcessToken(tokenizer);
        }

        private void ActionB(HtmlTokenizer tokenizer, BaseToken token)
        {
            // Process the token according to the rules given in the section for parsing tokens in foreign content.
            // TODO - 8.2.5.5 http://www.w3.org/TR/html5/syntax.html#parsing-main-inforeign
        }

        public void ProcessToken(HtmlTokenizer tokenizer, char c)
        {
            CharacterToken token = new CharacterToken() { Character = c };
            ProcessToken(tokenizer, token);
        }

        private bool IsNodeInNamespace(INode node, string @namespace)
        {
            // TODO - implement IsNodeInNamespace
            return true;
        }

        // http://www.w3.org/TR/html5/syntax.html#mathml-text-integration-point
        private bool IsMathMLIntegrationPoint(INode node)
        {
            throw new NotImplementedException();
        }

        // http://www.w3.org/TR/html5/syntax.html#html-integration-point
        private bool IsHTMLIntegrationPoint(INode node)
        {
            throw new NotImplementedException();
        }

        public void SaveCurrentInsertionModeState()
        {
            m_InsertionModeTokenProcessor.SaveCurrentInsertionModeState();
        }

        public BaseInsertionModeState GetOriginalInsertionModeState()
        {
            return m_InsertionModeTokenProcessor.OriginalInsertionMode;
        }

        public const string HTMLNamespace = "http://www.w3.org/1999/xhtml";
        public const string MathMLNamespace = "http://www.w3.org/1998/Math/MathML";
        public const string SVGNamespace = "http://www.w3.org/2000/svg";
        public const string XLinkNamespace = "http://www.w3.org/1999/xlink";
        public const string XMLNamespace = "http://www.w3.org/XML/1998/namespace";
        public const string XMLNSNamespace = "http://www.w3.org/2000/xmlns/";
    }
}

