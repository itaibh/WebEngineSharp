using System;

namespace WebEngineSharp.DOM
{
    // http://www.w3.org/TR/html5/dom.html#htmlelement
    public interface IHTMLElement : IElement
    {
        // metadata attributes
        string title { get; set; }
        string lang { get; set; }
        bool translate { get; set; }
        string dir { get; set; }
        DOMStringMap dataset { get; }


        // user interaction
        bool hidden { get; set; }
        void click();

        long tabIndex { get; set;}
        void focus();
        void blur();

        string accessKey { get; set;}
        string accessKeyLabel { get; }
        string contentEditable { get; set;}
        bool isContentEditable { get; }
        bool spellcheck { get; set;}
    }
}

