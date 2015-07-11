using System;

namespace WebEngineSharp.DOM.Impl
{
    public class HTMLTitleElement:HTMLElement, IHTMLTitleElement
    {
        public HTMLTitleElement(IDocument doc)
            : base(doc, "title")
        {
        }

        public string text { get; set; }
    }
}

