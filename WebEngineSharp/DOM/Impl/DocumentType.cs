using System;

namespace WebEngineSharp.DOM.Impl
{
    public class DocumentType : Node, IDocumentType
    {
        public DocumentType(IDocument doc, string name, string publicId, string systemId) 
            : base(doc)
        {
            this.name = name ?? string.Empty;
            this.publicId = publicId ?? string.Empty;
            this.systemId = systemId ?? string.Empty;
        }

        #region IDocumentType implementation

        public string name { get; private set; }

        public string publicId { get; private set; }

        public string systemId { get; private set; }

        #endregion
    }


    
}

