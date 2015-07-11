using System;
using System.IO;
using System.Text;
using WebEngineSharp.DOM;
using WebEngineSharp.DOM.Impl;

namespace WebEngineSharp.Parser
{
    public class HtmlParser : TokenParserBase
    {
        private HtmlParser()
        {
        }

        public override TokenParserBase[] PossibleNextToken
        {
            get { return new TokenParserBase[] { DocTypeNodeParser.Instance }; }
        }

        public override string Token
        {
            get { return null; }
        }

        public static IDocument Parse(Stream html)
        {
            HtmlParser parser = new HtmlParser();
            StreamReader reader = new StreamReader(html);
            return parser.Parse(reader);
        }

        private IDocument Parse(StreamReader reader)
        {


            int c = reader.Read();

            if (c == '<')
            {
                NodeParser tagParser = new NodeParser();
                tagParser.Parse(reader);
            }

            return null;
        }
    }
}

