using System;
using System.IO;
using System.Collections.Generic;

namespace WebEngineSharp.Parser
{
    public abstract class TokenParserBase
    {
        public virtual string Token { get { return null; } }

        public abstract TokenParserBase[] PossibleNextToken { get; }

        public void Parse(StreamReader reader)
        {
            if (Token != null)
            {
                reader.BaseStream.Seek(Token.Length, SeekOrigin.Current);
            }

            int longestTokenLength = 0;

            foreach (TokenParserBase token in PossibleNextToken)
            {
                if (token.Token != null)
                {
                    longestTokenLength = Math.Max(longestTokenLength, token.Token.Length);
                }
            }

            char[] buffer = new char[longestTokenLength];
            reader.ReadBlock(buffer, 0, longestTokenLength);
            reader.BaseStream.Seek(-longestTokenLength, SeekOrigin.Current);

            foreach (TokenParserBase token in PossibleNextToken)
            {
                if (token.Token == null)
                    continue;

                bool success = true;
                for (int i=0;i<token.Token.Length; ++i)
                {
                    char tc = token.Token[i];
                    char c = buffer[i];
                    if (Char.ToUpperInvariant(tc) != Char.ToUpperInvariant(c))
                    {
                        success = false;
                        break;
                    }
                }

                if (success)
                {
                    token.Parse(reader);
                }
            }
        }
    }
}

