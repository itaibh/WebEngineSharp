using System;

namespace WebEngineSharp.DOM
{
    public enum DocumentReadyState { loading, interactive, complete };

    public interface IDocument : INode, IGlobalEventHandlers
    {
        //[SameObject] 
        DOMImplementation implementation { get; }
        string URL { get; }
        string documentURI { get; }
        string origin { get; }
        string compatMode { get; }
        string characterSet { get; }
        string contentType { get; }

        IDocumentType doctype { get; }
        IElement documentElement { get; }
        HTMLCollection getElementsByTagName(string localName);
        HTMLCollection getElementsByTagNameNS(string @namespace, string localName);
        HTMLCollection getElementsByClassName(string classNames);

        //[NewObject] 
        IElement createElement(string localName);
        //[NewObject] 
        IElement createElementNS(string @namespace, string qualifiedName);
        //[NewObject]
        DocumentFragment createDocumentFragment();
        //[NewObject] 
        IText createTextNode(string data);
        //[NewObject]
        IComment createComment(string data);
        //[NewObject]
        ProcessingInstruction createProcessingInstruction(string target, string data);

        INode importNode(INode node, bool deep = false);
        INode adoptNode(INode node);

        //[NewObject] 
        IEvent createEvent(string @interface);

        //[NewObject]
        Range createRange();

        // NodeFilter.SHOW_ALL = 0xFFFFFFFF
        //[NewObject]
        NodeIterator createNodeIterator(INode root,  ulong whatToShow = 0xFFFFFFFF,  NodeFilter filter = null);
        //[NewObject]
        TreeWalker createTreeWalker(INode root, ulong whatToShow = 0xFFFFFFFF,  NodeFilter filter = null);

        //HTML5
        //--------------------
        object this[string name] { get; }

        //Attributes
        Location location { get; }
        string referrer { get; }
        string cookie { get; set; }
        string lastModified { get; }
        DocumentReadyState readyState{ get; }
        string title { get; set; }
        string dir  { get; set; }
        IHTMLElement body { get; set; }

        IHTMLHeadElement head { get; }
        HTMLCollection images { get; }
        HTMLCollection embeds { get; }
        HTMLCollection plugins { get; }
        HTMLCollection links { get; }
        HTMLCollection forms { get; }
        HTMLCollection scripts { get; }

        NodeList getElementsByName(string elementName);

        // dynamic markup insertion
        IDocument open(string type = "text/html", string replace = "");
        WindowProxy open(string url, string name, string features, bool replace = false);

        void close();
        void write(params string[] text);
        void writeln(params string[] text);
        // user interaction
        WindowProxy defaultView { get; }
        IElement activeElement { get; }

        bool hasFocus();

        string designMode { get; set; }

        bool execCommand(string commandId, bool showUI = false, string value = "");
        bool queryCommandEnabled(string commandId);
        bool queryCommandIndeterm(string commandId);
        bool queryCommandState(string commandId);
        bool queryCommandSupported(string commandId);
        string queryCommandValue(string commandId);

        // special event handler IDL attributes that only apply to Document objects
        //[LenientThis]
        event EventHandler onreadystatechange;

    }
}

