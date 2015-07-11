using System;

namespace WebEngineSharp.DOM
{
    // 4.2.3 http://www.w3.org/TR/html5/document-metadata.html#the-base-element
    public interface IHTMLBaseElement : IHTMLElement
    {
        string href { get; set; }
        string target { get; set; }
    }
}

