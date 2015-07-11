using System;

namespace WebEngineSharp.DOM.Impl
{
    public class HtmlElementFactory
    {
        private HtmlElementFactory()
        {
        }

        private static HtmlElementFactory s_Isntance = new HtmlElementFactory();

        public static HtmlElementFactory Instance
        {
            get{ return s_Isntance; }
        }

        public INode CreateTextNode(string data, IDocument doc)
        {
            return new Text(doc, data);
        }

        public INode CreateElement(string elementTagName, IDocument doc)
        {
            switch (elementTagName)
            {
                case "head":
                    return new HTMLHeadElement(doc);

                case "title":
                    return new HTMLTitleElement(doc);

                case "a":
                    return new HTMLAnchorElement(doc);

                case "q":
                    return new HTMLQuoteElement(doc);

                case "data":
                    return new HTMLDataElement(doc);

                case "time":
                    return new HTMLTimeElement(doc);

                case "span":
                    return new HTMLSpanElement(doc);

                case "br":
                    return new HTMLBRElement(doc);

                default:
                    return new HTMLElement(doc, elementTagName);
            }
        }
    }
}

