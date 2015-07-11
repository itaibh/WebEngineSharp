using System;

namespace WebEngineSharp.DOM.Impl
{
    public class HTMLHeadElement : HTMLElement, IHTMLHeadElement
    {
        public HTMLHeadElement(IDocument doc)
            : base(doc, "head")
        {
        }
    }
}

