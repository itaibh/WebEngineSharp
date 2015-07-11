using System;
using System.Collections.Specialized;
using System.IO;

namespace WebEngineSharp.Tokenizer
{
    public class DocTypeToken : BaseToken
    {
        public string Name { get; set; }

        public string PublicIdentifier { get; set; }

        public string SystemIdentifier { get; set; }

        public bool ForceQuirks { get; set; }

        public override string ToString()
        {
            return string.Format("[DocTypeToken: Name={0}, PublicIdentifier={1}, SystemIdentifier={2}, ForceQuirks={3}]", Name, PublicIdentifier, SystemIdentifier, ForceQuirks);
        }
    }
    
}
