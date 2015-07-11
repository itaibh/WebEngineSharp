using System;

namespace WebEngineSharp.Parser
{
    public class DocTypeNodeParser : TokenParserBase
    {
        private static DocTypeNodeParser s_Instance = new DocTypeNodeParser();

        public static DocTypeNodeParser Instance
        {
            get { return s_Instance; }
        }

        public override string Token
        {
            get { return "<!DOCTYPE"; }
        }

        public override TokenParserBase[] PossibleNextToken
        { 
            get {
                return new TokenParserBase[] { 
                    //DocumentParser.Instance 
                };
            }
        }
    }

    public class TextNodeParser : TokenParserBase
    {
        public override TokenParserBase[] PossibleNextToken
        { 
            get {
                return new TokenParserBase[] { 
                    //DocumentParser.Instance 
                };
            }
        }
    }
}

