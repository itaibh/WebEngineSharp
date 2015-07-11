using System;

namespace WebEngineSharp.DOM.Impl
{
    // 4.5.7 http://www.w3.org/TR/html5/text-level-semantics.html#the-q-element
    public class HTMLQuoteElement : HTMLElement
    {
        public HTMLQuoteElement(IDocument doc)
            : base(doc, "q")
        {
        }

        public string cite { get; set; }
    }
}

