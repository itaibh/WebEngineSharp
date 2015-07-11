using System;
using System.IO;

namespace WebEngineSharp.Tokenizer.States
{
    // 8.2.4.10 http://www.w3.org/TR/html5/syntax.html#tag-name-state
    public class TagNameState : BaseState
    {
        private TagNameState()
        {
        }

        private static TagNameState s_Instance = new TagNameState();

        public static TagNameState Instance
        {
            get { return s_Instance; }
        }

        public TagToken Token { get; set; }

        public override BaseState Process(HtmlTokenizer tokenizer, StreamReader reader)
        {
            for (;;)
            {
                int c = Read(reader);
                if (IsWhitespace(c))
                {
                    BeforeAttributeNameState.Instance.Token = Token;
                    return BeforeAttributeNameState.Instance;
                } 

                if (c == '/')
                {
                    return SelfClosingStartTagState.Instance;
                } 

                if (c == '>')
                {
                    tokenizer.EmitToken(Token);
                    return DataState.Instance;
                }

                if (base.IsUppercaseAsciiLetter(c))
                {
                    // Append the lowercase version of the current input character (add 0x0020 to the character's code point) to the current tag token's tag name.
                    Token.TagName += Char.ToLower((char)c);
                    continue;
                } 

                if (c == 0)
                {
                    // Parse error. Append a U+FFFD REPLACEMENT CHARACTER character to the current tag token's tag name.
                    ReportParseError();
                    Token.TagName += "\uFFFD";
                    continue;
                } 

                if (c == -1)
                {
                    ReportParseError();
                    return DataState.Instance;
                    // Reconsume the EOF character (?)
                }

                // Append the current input character to the current tag token's tag name.
                Token.TagName += (char)c;
            }
        }
    }
}

