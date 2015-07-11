using System;

namespace WebEngineSharp.DOM.Impl
{
    // http://www.w3.org/TR/html5/document-metadata.html#the-link-element
    public class HTMLLinkElement : HTMLElement, IHTMLLinkElement
    {
        public HTMLLinkElement(IDocument doc)
            : base(doc, "link")
        {
        }

        public bool disabled { get; set; }
        public string href { get; set; }
        public string crossOrigin { get; set; }
        public string rel { get; set; }
        public string rev { get; set; }
        public DOMTokenList relList { get; private set; }
        public string media { get; set; }
        public string hreflang { get; set; }
        public string type { get; set; }

        //[PutForwards=value]
        public DOMSettableTokenList sizes { get; private set; }

        #region ILinkStyle implementation

        public StyleSheet sheet { get; set;}

        #endregion
    }
}

