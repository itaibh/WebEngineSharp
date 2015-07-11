using System;
using System.Collections.Specialized;
using System.IO;

namespace WebEngineSharp.Tokenizer
{
    public class EndTagToken : TagToken
    {
        public override string ToString()
        {
            return string.Format("[EndTagToken: TagName={0}]", TagName);
        }
    }
    
}
