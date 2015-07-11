using System;
using System.Collections.Generic;
using System.IO;

namespace WebEngineSharp.Tokenizer
{
    public class AttributeToken : BaseToken
	{
        public string AttributeName { get; set; }
        public string AttributeValue { get; set; }
        public TagToken ContainingTag { get; set; }

        public override string ToString()
        {
            return string.Format("[AttributeToken: AttributeName={0}, AttributeValue={1}, ContainingTag={2}]", AttributeName, AttributeValue, ContainingTag);
        }
	}
}
