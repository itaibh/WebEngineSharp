using System;

namespace WebEngineSharp.DOM.Impl
{
    // 5.4 http://www.w3.org/TR/dom/#interface-node
    public class Node : EventTarget, INode
    {
        public Node(IDocument document)
        {
            ownerDocument = document;
            childNodes = new NodeList();
        }

        public const ushort ELEMENT_NODE = 1;
        public const ushort ATTRIBUTE_NODE = 2;
        // historical
        public const ushort TEXT_NODE = 3;
        public const ushort CDATA_SECTION_NODE = 4;
        // historical
        public const ushort ENTITY_REFERENCE_NODE = 5;
        // historical
        public const ushort ENTITY_NODE = 6;
        // historical
        public const ushort PROCESSING_INSTRUCTION_NODE = 7;
        public const ushort COMMENT_NODE = 8;
        public const ushort DOCUMENT_NODE = 9;
        public const ushort DOCUMENT_TYPE_NODE = 10;
        public const ushort DOCUMENT_FRAGMENT_NODE = 11;
        public const ushort NOTATION_NODE = 12;
        // historical

        public const ushort DOCUMENT_POSITION_DISCONNECTED = 0x01;
        public const ushort DOCUMENT_POSITION_PRECEDING = 0x02;
        public const ushort DOCUMENT_POSITION_FOLLOWING = 0x04;
        public const ushort DOCUMENT_POSITION_CONTAINS = 0x08;
        public const ushort DOCUMENT_POSITION_CONTAINED_BY = 0x10;
        public const ushort DOCUMENT_POSITION_IMPLEMENTATION_SPECIFIC = 0x20;

        #region INode implementation

        public bool hasChildNodes()
        {
            throw new NotImplementedException();
        }

        public void normalize()
        {
            throw new NotImplementedException();
        }

        public INode cloneNode(bool deep = false)
        {
            throw new NotImplementedException();
        }

        public bool isEqualNode(INode node)
        {
            throw new NotImplementedException();
        }

        public ushort compareDocumentPosition(INode other)
        {
            throw new NotImplementedException();
        }

        public bool contains(INode other)
        {
            throw new NotImplementedException();
        }

        public string lookupPrefix(string @namespace)
        {
            throw new NotImplementedException();
        }

        public string lookupNamespaceURI(string prefix)
        {
            throw new NotImplementedException();
        }

        public bool isDefaultNamespace(string @namespace)
        {
            throw new NotImplementedException();
        }

        public INode insertBefore(INode node, INode child)
        {
            // 1. Ensure pre-insertion validity of node into parent before child.
            EnsurePreInsertionValidity(node);

            // 2. Let reference child be child.

            // 3. If reference child is node, set it to node's next sibling.

            // 4. Adopt node into parent's node document.

            // 5. Insert node into parent before reference child.

            // 6. Return node.
            return child;
        }

        public INode appendChild(INode node)
        {
            // TODO - when insertBefore is completely implemented, use it instead of the code below.
            //return insertBefore(null, node);

            childNodes.Append(node);
            return node;
        }

        private void EnsurePreInsertionValidity(INode node)
        {
            // 1. If parent is not a Document, DocumentFragment, or Element node, throw a "HierarchyRequestError".
            //
            // 2. If node is a host-including inclusive ancestor of parent, throw a "HierarchyRequestError".
            //
            // 3. If child is not null and its parent is not parent, throw a "NotFoundError" exception.
            //
            // 4. If node is not a DocumentFragment, DocumentType, Element, Text, ProcessingInstruction, or Comment node,
            //    throw a "HierarchyRequestError".
            //
            // 5. If either node is a Text node and parent is a document, or node is a doctype and parent is not a document,
            //    throw a "HierarchyRequestError".
            //
            // 6. If parent is a document, and any of the statements below, switched on node, are true, throw a
            //    "HierarchyRequestError".
            //
            //    * DocumentFragment node
            //          If node has more than one element child or has a Text node child.
            //          Otherwise, if node has one element child, and parent has an element child, child is a doctype, or child
            //          is not null and a doctype is following child.
            //
            //    * element
            //          parent has an element child, child is a doctype, or child is not null and a doctype is following child.
            //
            //    * doctype
            //          parent has a doctype child, an element is preceding child, or child is null and parent has an element
            //          child.
        }

        public INode replaceChild(INode node, INode child)
        {
            throw new NotImplementedException();
        }

        public INode removeChild(INode child)
        {
            throw new NotImplementedException();
        }

        public ushort nodeType { get; private set; }

        public string nodeName { get; private set; }

        public string baseURI { get; private set; }

        public IDocument ownerDocument { get; private set; }

        public INode parentNode { get; private set; }

        public IElement parentElement { get; private set; }

        public NodeList childNodes { get; private set; }

        public INode firstChild { get; private set; }

        public INode lastChild
        {
            get { return childNodes.length > 0 ? childNodes[childNodes.length - 1] : null; }
        }

        public INode previousSibling
        {
            get {
                if (parentNode == null || parentNode.childNodes == null)
                {
                    return null;
                }

                int idx = parentNode.childNodes.IndexOf(this);
                if (idx >= 1)
                {
                    return parentNode.childNodes[(uint)(idx - 1)];
                }
                return null;
            }
        }

        public INode nextSibling
        {
            get {
                if (parentNode == null || parentNode.childNodes == null)
                {
                    return null;
                }

                int idx = parentNode.childNodes.IndexOf(this);
                if (idx >= 0 && (ulong)idx < parentNode.childNodes.length - 1)
                {
                    return parentNode.childNodes[(uint)(idx + 1)];
                }
                return null;
            }
        }

        public string nodeValue { get; set; }

        public string textContent { get; set; }

        #endregion
    }
}

