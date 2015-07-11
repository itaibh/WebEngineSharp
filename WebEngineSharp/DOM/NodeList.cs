using System;
using System.Collections.Generic;

namespace WebEngineSharp.DOM
{
    // http://www.w3.org/TR/dom/#interface-nodelist
	public class NodeList
	{
        private List<INode> m_collection = new List<INode>(); 
       
        public ulong length { get { return  (ulong)m_collection.Count; } }
       
        public INode item(ulong index)
        {
            int idx = (int)index;
            if (idx >= m_collection.Count)
                return null;

            return m_collection[idx];
        }

        public INode this[ulong index]
        {
            get { return item(index); }
        }

        internal void Append(INode node)
        {
            m_collection.Add(node);
        }
	}
}
