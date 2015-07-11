using System;

namespace WebEngineSharp.DOM.Impl
{
    public class HTMLHtmlElement : HTMLElement, IHTMLHtmlElement
    {
        public HTMLHtmlElement(IDocument doc)
            : base(doc, "html")
        {
        }
    }
}

