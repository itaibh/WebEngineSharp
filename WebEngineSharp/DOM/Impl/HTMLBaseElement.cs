using System;

namespace WebEngineSharp.DOM.Impl
{
    // 4.2.3 http://www.w3.org/TR/html5/document-metadata.html#the-base-element
    public class HTMLBaseElement : HTMLElement, IHTMLBaseElement
    {
        public HTMLBaseElement(IDocument doc)
            : base(doc, null)
        {
        }

        public string href { get; set; }
        public string target { get; set; }
    }
}

