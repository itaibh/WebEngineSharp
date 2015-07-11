using System;
using System.IO;
using WebEngineSharp.DOM;

namespace WebEngineSharp.Parser
{
    public class NodeParser
    {
        public NodeParser()
        {
        }

        public INode Parse(StreamReader reader)
        {
            HtmlToken token = ConsumeTagToken(reader);
            if (token == HtmlToken.OpenMetaTag)
            {
                CommentParser commentParser = new CommentParser();
                string comment = commentParser.ParseCommentText(reader);
            }
            else if (token == HtmlToken.OpenComment)
            {
                CommentParser commentParser = new CommentParser();
                string comment = commentParser.ParseCommentText(reader);
            }
            else if (token == HtmlToken.OpenTag)
            {
                ConsumeTagName(reader);
            }

            int c = reader.Read();
            while (c > -1)
            {
                if (c == '>')
                {
                }
                c = reader.Read();
            }
            return null;
        }

        private HtmlToken ConsumeTagToken(StreamReader reader)
        {
            int c = reader.Peek();
            if (c != '!')
            {
                return HtmlToken.OpenTag;
            }   

            reader.Read();
            c = reader.Peek();
            if (c != '-')
            {
                return HtmlToken.OpenMetaTag;
            }

            reader.Read();
            c = reader.Peek();
            if (c == '-')
            {
                return HtmlToken.OpenComment;
            }
            return HtmlToken.OpenComment;
        }

        private void ConsumeTagName(StreamReader reader)
        {
            throw new NotImplementedException();
        }
    }
}

