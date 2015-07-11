using System;

namespace WebEngineSharp.DOM
{
	public class Attr
	{
        public string localName { get; private set; }
        public string value { get; set; }

        public string name { get; private set; }
        public string namespaceURI { get; private set; }
        public string prefix { get; private set; }

        public bool specified { get { return true; } } // useless; always returns true
	}

}

