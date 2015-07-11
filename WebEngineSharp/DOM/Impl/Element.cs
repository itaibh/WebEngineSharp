using System;

namespace WebEngineSharp.DOM.Impl
{
    public class Element : Node, IElement
    {
        internal Element(IDocument doc, string tagName)
            : base(doc)
        {
            this.tagName = tagName;
        }

        #region IElement implementation

        public string getAttribute(string name)
        {
            throw new NotImplementedException();
        }

        public string getAttributeNS(string @namespace, string localName)
        {
            throw new NotImplementedException();
        }

        public void setAttribute(string name, string value)
        {
            throw new NotImplementedException();
        }

        public void setAttributeNS(string @namespace, string name, string value)
        {
            throw new NotImplementedException();
        }

        public void removeAttribute(string name)
        {
            throw new NotImplementedException();
        }

        public void removeAttributeNS(string @namespace, string localName)
        {
            throw new NotImplementedException();
        }

        public bool hasAttribute(string name)
        {
            throw new NotImplementedException();
        }

        public bool hasAttributeNS(string @namespace, string localName)
        {
            throw new NotImplementedException();
        }

        public bool matches(string selectors)
        {
            throw new NotImplementedException();
        }

        public HTMLCollection getElementsByTagName(string localName)
        {
            throw new NotImplementedException();
        }

        public HTMLCollection getElementsByTagNameNS(string @namespace, string localName)
        {
            throw new NotImplementedException();
        }

        public HTMLCollection getElementsByClassName(string classNames)
        {
            throw new NotImplementedException();
        }

        public string namespaceURI  { get; private set; }
        public string prefix { get; private set; }
        public string localName { get; private set; }
        public string tagName { get; private set; }

        public string id { get; set;}
        public string className { get; set;}

        public DOMTokenList classList
        {
            get {
                throw new NotImplementedException();
            }
        }

        public Attr[] attributes
        {
            get {
                throw new NotImplementedException();
            }
        }

        #endregion
    }
}

