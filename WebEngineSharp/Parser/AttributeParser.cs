using System;
using System.IO;

namespace WebEngineSharp.Parser
{
    public class AttributeParser
    {
        public AttributeParser()
        {
        }

        public void Parse(StreamReader reader)
        {
            int c = reader.Read();
            while (c > -1)
            {
                c = reader.Read();
            }
        }
    }
}

