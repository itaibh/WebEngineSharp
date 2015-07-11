using System;
using WebEngineSharp.DOM;
using WebEngineSharp.DOM.Impl;

namespace WebEngineSharp.Tokenizer.InsertionMode
{
    public abstract class BaseInsertionModeState
    {
        public abstract BaseInsertionModeState ProcessToken(HtmlTokenizer tokenizer, ITokenQueue queue, BaseToken token, IDocument doc);

        protected bool IsWhitespace(BaseToken token)
        {
            CharacterToken charToken = token as CharacterToken;
            if (charToken == null)
            {
                return false;
            }

            return IsWhitespace(charToken);
        }

        protected bool IsWhitespace(CharacterToken charToken)
        {
            char c = charToken.Character;
            return (c == 0x09 || c == 0x0A || c == 0x0C || c == 0x0D || c == ' ');
        }

        // http://www.w3.org/TR/html5/syntax.html#parse-error
        public void ReportParseError()
        {
        }

        // http://www.w3.org/TR/html5/syntax.html#insert-a-comment
        public void InsertComment(CommentToken token, IDocument doc)
        {
            //TODO - make sure the steps conform with the specs in the link above.
            IComment commentNode = new Comment(doc, token.Comment);
            doc.appendChild(commentNode);
        }

        // http://www.w3.org/TR/html5/syntax.html#insert-an-html-element
        public INode InsertHtmlElement(TagToken token, IDocument doc)
        {
            //TODO - make sure the steps conform with the specs in the link above.
            INode element = HtmlElementFactory.Instance.CreateElement(token.TagName, doc);
            return element;
        }

        // http://www.w3.org/TR/html5/syntax.html#insert-a-character
        public INode InsertCharacter(CharacterToken token, IDocument doc)
        {
            //TODO - make sure the steps conform with the specs in the link above.
            INode element = HtmlElementFactory.Instance.CreateTextNode(token.Character.ToString(), doc);

            return element;
        }
    }
}
