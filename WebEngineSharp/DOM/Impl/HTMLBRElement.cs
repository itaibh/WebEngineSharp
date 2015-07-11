using System;

namespace WebEngineSharp.DOM.Impl
{
    // 4.5.29 http://www.w3.org/TR/html5/text-level-semantics.html#the-br-element
    public class HTMLBRElement: HTMLElement
    {
        public HTMLBRElement(IDocument doc) : base(doc, "br")
        {
        }
    }

}

