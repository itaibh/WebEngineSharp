using System;

namespace WebEngineSharp.DOM.Impl
{
    // 4.5.28 http://www.w3.org/TR/html5/text-level-semantics.html#the-span-element
    public class HTMLSpanElement: HTMLElement
    {
        public HTMLSpanElement(IDocument doc) : base(doc, "span")
        {
        }
    }

}

