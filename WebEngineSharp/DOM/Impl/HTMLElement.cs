using System;

namespace WebEngineSharp.DOM.Impl
{
    //  http://www.w3.org/TR/html5/dom.html#htmlelement
    public class HTMLElement : Element, IHTMLElement
    {
        internal HTMLElement(IDocument doc, string tagName)
            :base(doc, tagName)
        {
        }

        // metadata attributes
        public string title { get; set; }
        public string lang { get; set; }
        public bool translate { get; set; }
        public string dir { get; set; }
        public DOMStringMap dataset { get; private set; }


        // user interaction
        public bool hidden { get; set; }
        public void click()
        {
        }

        public long tabIndex { get; set;}
        public void focus()
        {
        }

        public void blur()
        {
        }

        public string accessKey { get; set;}
        public string accessKeyLabel { get; private set;}
        public string contentEditable { get; set;}
        public bool isContentEditable { get; private set;}
        public bool spellcheck { get; set;}
    }
}
