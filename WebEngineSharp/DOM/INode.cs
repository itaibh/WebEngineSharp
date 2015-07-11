using System;

namespace WebEngineSharp.DOM
{
    // 5.4 http://www.w3.org/TR/dom/#interface-node
    public interface INode : IEventTarget
	{
        ushort nodeType { get; }
        string nodeName { get; }

        string baseURI { get; }

        IDocument ownerDocument { get; }
        INode parentNode { get; }
        IElement parentElement { get; }
        bool hasChildNodes();

        //[SameObject]
        NodeList childNodes { get; }
        INode firstChild { get; }
        INode lastChild { get; }
        INode previousSibling { get; }
        INode nextSibling { get; }

        string nodeValue { get; set; }
        string textContent { get; set; }
        void normalize();

        INode cloneNode(bool deep = false);
        bool isEqualNode(INode node);

        ushort compareDocumentPosition(INode other);
        bool contains(INode other);

        string lookupPrefix(string @namespace);
        string lookupNamespaceURI(string prefix);
        bool isDefaultNamespace(string @namespace);

        INode insertBefore(INode node, INode child);
        INode appendChild(INode node);
        INode replaceChild(INode node, INode child);
        INode removeChild(INode child);
	}

}

