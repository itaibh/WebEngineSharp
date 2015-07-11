using System;

namespace WebEngineSharp.DOM
{
    // 4.2.4 http://www.w3.org/TR/html5/document-metadata.html#the-link-element
    public interface IHTMLLinkElement : IHTMLElement, ILinkStyle
    {
        bool disabled { get; set; }
        string href { get; set; }
        string crossOrigin { get; set; }
        string rel { get; set; }
        string rev { get; set; }
        DOMTokenList relList { get; }
        string media { get; set; }
        string hreflang { get; set; }
        string type { get; set; }

        //[PutForwards=value]
        DOMSettableTokenList sizes { get; }

    }
}

