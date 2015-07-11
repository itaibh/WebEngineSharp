using System;
using System.IO;
using System.Text;

namespace WebEngineSharp.Parser
{
    public class CommentParser
    {
        public CommentParser()
        {
        }

        private static StringBuilder sb = new StringBuilder(400);

        public string ParseCommentText(StreamReader reader)
        {
            sb.Length = 0;

            int c = reader.Read();
            while (c > -1)
            {
                c = reader.Read();
                if (c == '-')
                {
                    c = reader.Read();
                    if (c == '-')
                    {
                        c = reader.Read();
                        if (c == '>')
                        {
                            break;
                        }
                        sb.Append('-');
                    }
                    sb.Append('-');
                }
                sb.Append((char)c);
            }

            return sb.ToString();
        }
    }
}

