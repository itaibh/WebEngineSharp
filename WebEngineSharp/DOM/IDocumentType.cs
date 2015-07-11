using System;

namespace WebEngineSharp.DOM
{
    // 5.7 http://www.w3.org/TR/dom/#interface-documenttype
	public interface IDocumentType : INode
    {
        string name { get; }
        string publicId { get; }
        string systemId { get; }
	}
}

