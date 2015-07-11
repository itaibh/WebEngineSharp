using System;

namespace WebEngineSharp.Parser
{
    public class DocumentParser : TokenParserBase
    {
        private static DocumentParser s_Instance = new DocumentParser();

        public static DocumentParser Instance
        {
            get { return s_Instance; }
        }

        public override string Token
        {
            get { return "<html"; }
        }

        public override TokenParserBase[] PossibleNextToken
        {
            get {
                return new TokenParserBase[] { };
            }
        }
    }

}

