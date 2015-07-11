using System;

namespace WebEngineSharp.DOM.Impl
{
    // 4.5.1 http://www.w3.org/TR/html5/text-level-semantics.html#the-a-element
    public class HTMLAnchorElement : HTMLElement
    {
        public HTMLAnchorElement(IDocument doc)
            : base(doc, "a")
        {
            relList = new DOMTokenList();
        }

        public string target { get; set; }

        public string download { get; set; }

        public string rel { get; set; }

        public string rev { get; set; }

        public DOMTokenList relList { get; private set; }

        public string hreflang { get; set; }

        public string type { get; set; }

        public string text { get; set; }
    }
}

