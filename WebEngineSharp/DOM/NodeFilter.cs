using System;

namespace WebEngineSharp.DOM
{
	public class NodeFilter
	{
        const ulong SHOW_ALL = 0xFFFFFFFF;
        const ulong SHOW_ELEMENT = 0x1;
        const ulong SHOW_ATTRIBUTE = 0x2; // historical
        const ulong SHOW_TEXT = 0x4;
        const ulong SHOW_CDATA_SECTION = 0x8; // historical
        const ulong SHOW_ENTITY_REFERENCE = 0x10; // historical
        const ulong SHOW_ENTITY = 0x20; // historical
        const ulong SHOW_PROCESSING_INSTRUCTION = 0x40;
        const ulong SHOW_COMMENT = 0x80;
        const ulong SHOW_DOCUMENT = 0x100;
        const ulong SHOW_DOCUMENT_TYPE = 0x200;
        const ulong SHOW_DOCUMENT_FRAGMENT = 0x400;
        const ulong SHOW_NOTATION = 0x800; // historical

        public ushort acceptNode(INode node)
        {
            throw new NotImplementedException();
        }
	}


    
}

