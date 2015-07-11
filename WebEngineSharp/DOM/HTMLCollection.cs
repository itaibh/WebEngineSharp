using System;
using System.Collections.Generic;

namespace WebEngineSharp.DOM
{
    // http://www.w3.org/TR/dom/#dom-htmlcollection-item
	public class HTMLCollection
	{
        private List<IElement> m_collection = new List<IElement>(); 

        public ulong length { get { return (ulong)m_collection.Count; } }

        public IElement item(ulong index)
        {
            int idx = (int)index;
            if (idx >= m_collection.Count)
                return null;

            return m_collection[idx];
        }

        public IElement this[ulong index] { get { return item(index); } }

        public IElement namedItem(string name)
        {
            if (string.IsNullOrEmpty(name))
                return null;

            foreach (IElement elem in m_collection)
            {
                if (elem.id == name)
                    return elem;

                if (elem.getAttribute("name") == name)
                    return elem;
            }

            return null;
        }

        public IElement this[string name] { get { return namedItem(name); } }
	}
}
