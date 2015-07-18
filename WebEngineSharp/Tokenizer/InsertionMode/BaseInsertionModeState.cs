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

            //1. Let the adjusted insertion location be the appropriate place for inserting a node.
            InsertionLocation adjustedInsertionLocation = GetAppropriatePlaceForInsertingANode();

            //2. Create an element for the token in the given namespace, with the intended parent being
            //   the element in which the adjusted insertion location finds itself.
            INode element = HtmlElementFactory.Instance.CreateElement(token.TagName, doc);

            //3. TODO - If it is possible to insert an element at the adjusted insertion location, then insert
            //   TODO - the newly created element at the adjusted insertion location.
            //   NOTE: If the adjusted insertion location cannot accept more elements, e.g. because it's a Document
            //         that already has an element child, then the newly created element is dropped on the floor.

            //4. Push the element onto the stack of open elements so that it is the new current node.
            TreeConstruction.Instance.StackOfOpenElements.Push(element);

            //5. Return the newly created element.
            return element;
        }

        // http://www.w3.org/TR/html5/syntax.html#insert-a-character
        public INode InsertCharacter(CharacterToken token, IDocument doc)
        {
            //TODO - make sure the steps conform with the specs in the link above.

            //1. Let data be the characters passed to the algorithm, or, if no characters were explicitly
            //   specified, the character of the character token being processed.

            //2. Let the adjusted insertion location be the appropriate place for inserting a node.
            InsertionLocation adjustedInsertionLocation = GetAppropriatePlaceForInsertingANode();

            //3. If the adjusted insertion location is in a Document node, then abort these steps.
            //   NOTE: The DOM will not let Document nodes have Text node children, so they are dropped on the floor.
            if (adjustedInsertionLocation.Parent == doc)
            {
                return null;
            }

            //4. If there is a Text node immediately before the adjusted insertion location, then append data to
            //   that Text node's data.
            //   Otherwise, create a new Text node whose data is data and whose ownerDocument is the same as that
            //   of the element in which the adjusted insertion location finds itself, and insert the newly created
            //   node at the adjusted insertion location.
            IText prevTextNode = null;
            if (adjustedInsertionLocation.Child != null)
            {
                if (!adjustedInsertionLocation.InsertBefore)
                {
                    prevTextNode = adjustedInsertionLocation.Child as IText;
                }
                else{
                    prevTextNode = adjustedInsertionLocation.Child.previousSibling as IText;
                }
            } else
            {
                prevTextNode = adjustedInsertionLocation.Parent.lastChild as IText;
            } 

            if (prevTextNode != null)
            {
                prevTextNode.appendData(token.Character.ToString());
                return prevTextNode;
            } else
            {
                INode element = HtmlElementFactory.Instance.CreateTextNode(token.Character.ToString(), 
                                    adjustedInsertionLocation.Parent.ownerDocument);
                adjustedInsertionLocation.Parent.appendChild(element);
                return element;
            }
        }

        // http://www.w3.org/TR/html5/syntax.html#appropriate-place-for-inserting-a-node
        private InsertionLocation GetAppropriatePlaceForInsertingANode()
        {
            //1. If there was an override target specified, then let target be the override target.
            //   Otherwise, let target be the current node. (http://www.w3.org/TR/html5/syntax.html#current-node)

            INode target = TreeConstruction.Instance.StackOfOpenElements.Peek();
            InsertionLocation adjustedInsertionLocation = new InsertionLocation();

            bool isFosterParentingEnabled = false;
            if (isFosterParentingEnabled && (
                    target is IHTMLTableElement ||
                    target is IHTMLTableSectionElement ||
                    target is IHTMLTableRowElement))
            {
                // NOTE: Foster parenting happens when content is misnested in tables.
                // 
                // 1. Let last template be the last template element in the stack of open elements, if any.
                // 
                // 2. Let last table be the last table element in the stack of open elements, if any.
                // 
                // 3. If there is a last template and either there is no last table, or there is one, but last
                //    template is lower (more recently added) than last table in the stack of open elements, then:
                //    let adjusted insertion location be inside last template's template contents, after its last
                //    child (if any), and abort these substeps.
                // 
                // 4. If there is no last table, then let adjusted insertion location be inside the first element in
                //    the stack of open elements (the html element), after its last child (if any), and abort these
                //    substeps. (fragment case)
                // 
                // 5. If last table has a parent element, then let adjusted insertion location be inside last table's
                //    parent element, **immediately before** last table, and abort these substeps.
                // 
                // 6. Let previous element be the element immediately above last table in the stack of open elements.
                // 
                // 7. Let adjusted insertion location be inside previous element, after its last child (if any).
                // 
                // NOTE: These steps are involved in part because it's possible for elements, the table element in this
                //       case in particular, to have been moved by a script around in the DOM, or indeed removed from the
                //       DOM entirely, after the element was inserted by the parser.
            } else
            {
                //Let adjusted insertion location be inside target, after its last child (if any).
                adjustedInsertionLocation.Parent = target;
                adjustedInsertionLocation.Child = target.lastChild;
            }

            //3. If the adjusted insertion location is inside a template element, let it instead be inside the
            //   template element's template contents, after its last child (if any).

            //4. Return the adjusted insertion location.
            return adjustedInsertionLocation;
        }

        // http://www.w3.org/TR/html5/syntax.html#stop-parsing
        public void StopParsing()
        {
            //TODO - make sure the steps conform with the specs in the link above.
        }

        class InsertionLocation
        {
            public INode Parent { get; set; }

            public INode Child { get; set; }

            public bool InsertBefore { get; set; }
        }
    }
}
