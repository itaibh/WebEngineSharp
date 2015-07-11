using System;

namespace WebEngineSharp.DOM.Impl
{
    // 4.5.10 http://www.w3.org/TR/html5/text-level-semantics.html#the-data-element
    public class HTMLDataElement: HTMLElement
    {
        public HTMLDataElement(IDocument doc) : base(doc, "data")
        {
        }

        public string value { get; set; }
	}

}

