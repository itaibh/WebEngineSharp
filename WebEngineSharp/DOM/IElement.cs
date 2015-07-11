using System;

namespace WebEngineSharp.DOM
{
	public interface IElement : INode
	{
        string namespaceURI { get; }
        string prefix { get; }
        string localName { get; }
        string tagName { get; }

        string id  { get; set; }
        string className  { get; set; }

        //[SameObject]
        DOMTokenList classList {get;}
        //[SameObject]
        Attr[] attributes {get;}

        string getAttribute(string name);
        string getAttributeNS(string @namespace, string localName);
        void setAttribute(string name, string value);
        void setAttributeNS(string @namespace, string name, string value);
        void removeAttribute(string name);
        void removeAttributeNS(string @namespace, string localName);
        bool hasAttribute(string name);
        bool hasAttributeNS(string @namespace, string localName);

        bool matches(string selectors);

        HTMLCollection getElementsByTagName(string localName);
        HTMLCollection getElementsByTagNameNS(string @namespace, string localName);
        HTMLCollection getElementsByClassName(string classNames);
	}
}

