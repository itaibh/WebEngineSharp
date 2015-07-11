using System;
using System.Collections.Specialized;
using System.IO;

namespace WebEngineSharp.Tokenizer.States
{
    // 8.2.4.54 http://www.w3.org/TR/html5/syntax.html#doctype-name-state
    public class DocTypeNameState : BaseState
    {
        private DocTypeNameState()
        {
        }

        private static DocTypeNameState s_Instance = new DocTypeNameState();

        public static DocTypeNameState Instance
        {
            get { return s_Instance; }
        }

        public DocTypeToken Token { get; set; }

        public override BaseState Process(HtmlTokenizer tokenizer, StreamReader reader)
        {
            for (;;)
            {
                int c = Read(reader);
                if (IsWhitespace(c))
                {
                    AfterDocTypeNameState.Instance.Token = Token;
                    return AfterDocTypeNameState.Instance;
                } 

                if (c == '>')
                {
                    tokenizer.EmitToken(Token);
                    return DataState.Instance;
                }

                if (base.IsUppercaseAsciiLetter(c))
                {
                    // Append the lowercase version of the current input character (add 0x0020 to the character's code point) to the current tag token's tag name.
                    Token.Name += Char.ToLower((char)c);
                    continue;
                } 

                if (c == 0)
                {
                    // Parse error. Append a U+FFFD REPLACEMENT CHARACTER character to the current tag token's tag name.
                    ReportParseError();
                    Token.Name += "\uFFFD";
                    continue;
                } 

                if (c == -1)
                {
                    // Parse error. Switch to the data state. Reconsume the EOF character.
                    ReportParseError();
                    Token.ForceQuirks = true;
                    tokenizer.EmitToken(Token);
                    return DataState.Instance;
                }

                // Append the current input character to the current tag token's tag name.
                Token.Name += (char)c;
            }
        }
    }
}
