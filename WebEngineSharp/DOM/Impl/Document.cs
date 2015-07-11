using System;

namespace WebEngineSharp.DOM.Impl
{
    public class Document : Node, IDocument
    {
        public Document() : base(null)
        {
        }

        public HTMLCollection getElementsByTagName(string localName)
        {
            throw new NotImplementedException();
        }

        public HTMLCollection getElementsByTagNameNS(string @namespace, string localName)
        {
            throw new NotImplementedException();
        }

        public HTMLCollection getElementsByClassName(string classNames)
        {
            throw new NotImplementedException();
        }

        public IElement createElement(string localName)
        {
            throw new NotImplementedException();
        }

        public IElement createElementNS(string @namespace, string qualifiedName)
        {
            throw new NotImplementedException();
        }

        public DocumentFragment createDocumentFragment()
        {
            throw new NotImplementedException();
        }

        public IText createTextNode(string data)
        {
            throw new NotImplementedException();
        }

        public IComment createComment(string data)
        {
            throw new NotImplementedException();
        }

        public ProcessingInstruction createProcessingInstruction(string target, string data)
        {
            throw new NotImplementedException();
        }

        public INode importNode(INode node, bool deep = false)
        {
            throw new NotImplementedException();
        }

        public INode adoptNode(INode node)
        {
            throw new NotImplementedException();
        }

        public IEvent createEvent(string @interface)
        {
            throw new NotImplementedException();
        }

        public Range createRange()
        {
            throw new NotImplementedException();
        }

        public NodeIterator createNodeIterator(INode root, ulong whatToShow = 4294967295uL, NodeFilter filter = null)
        {
            throw new NotImplementedException();
        }

        public TreeWalker createTreeWalker(INode root, ulong whatToShow = 4294967295uL, NodeFilter filter = null)
        {
            throw new NotImplementedException();
        }

        public DOMImplementation implementation { get; private set; }

        public string URL { get; private set; }

        public string documentURI { get; private set; }

        public string origin { get; private set; }

        public string compatMode { get; private set; }

        public string characterSet { get; private set; }

        public string contentType { get; private set; }

        public IDocumentType doctype { get; private set; }

        public IElement documentElement { get; private set; }

        #region IDocument implementation

        public event EventHandler onreadystatechange;

        // http://www.w3.org/TR/html5/dom.html#dom-document-getelementsbyname
        public NodeList getElementsByName(string elementName)
        {
            throw new NotImplementedException();
        }

        public IDocument open(string type = "text/html", string replace = "")
        {
            throw new NotImplementedException();
        }

        public WindowProxy open(string url, string name, string features, bool replace = false)
        {
            throw new NotImplementedException();
        }

        public void close()
        {
            throw new NotImplementedException();
        }

        public void write(params string[] text)
        {
            throw new NotImplementedException();
        }

        public void writeln(params string[] text)
        {
            throw new NotImplementedException();
        }

        public bool hasFocus()
        {
            throw new NotImplementedException();
        }

        public bool execCommand(string commandId, bool showUI = false, string value = "")
        {
            throw new NotImplementedException();
        }

        public bool queryCommandEnabled(string commandId)
        {
            throw new NotImplementedException();
        }

        public bool queryCommandIndeterm(string commandId)
        {
            throw new NotImplementedException();
        }

        public bool queryCommandState(string commandId)
        {
            throw new NotImplementedException();
        }

        public bool queryCommandSupported(string commandId)
        {
            throw new NotImplementedException();
        }

        public string queryCommandValue(string commandId)
        {
            throw new NotImplementedException();
        }

        public object this[string name]
        {
            get {
                throw new NotImplementedException();
            }
        }

        public Location location { get; private set; }

        public string referrer { get; private set; }

        public string cookie { get; set; }

        public string lastModified { get; private set; }

        public DocumentReadyState readyState { get; private set; }

        public string title { get; set; }

        public string dir { get; set; }

        public IHTMLElement body { get; set; }

        public IHTMLHeadElement head { get; internal set; }

        public HTMLCollection images { get; private set; }

        public HTMLCollection embeds { get; private set; }

        public HTMLCollection plugins { get; private set; }

        public HTMLCollection links { get; private set; }

        public HTMLCollection forms { get; private set; }

        public HTMLCollection scripts { get; private set; }

        public WindowProxy defaultView { get; private set; }

        public IElement activeElement { get; private set; }

        public string designMode { get; set; }

        #endregion
    }
}

