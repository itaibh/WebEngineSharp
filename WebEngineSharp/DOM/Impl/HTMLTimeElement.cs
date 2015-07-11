using System;

namespace WebEngineSharp.DOM.Impl
{
    // 4.5.11 http://www.w3.org/TR/html5/text-level-semantics.html#the-time-element
    public class HTMLTimeElement: HTMLElement
    {
        public HTMLTimeElement(IDocument doc) : base(doc, "time")
        {
        }

        public string dateTime { get; set; }
	}

}

