using System;
using System.Collections.Generic;
using System.IO;

namespace WebEngineSharp.Tokenizer
{
    public abstract class TagToken : BaseToken
    {
        public TagToken()
        {
            Attributes = new List<AttributeToken>();
        }

        public string TagName { get; set; }
        public bool IsSelfClosing { get; set; }
        public List<AttributeToken> Attributes { get; set; }

        public override string ToString()
        {
            return string.Format("[TagToken: TagName={0}, IsSelfClosing={1}, Attributes={2}]", TagName, IsSelfClosing, Attributes);
        }
    }
    
}
