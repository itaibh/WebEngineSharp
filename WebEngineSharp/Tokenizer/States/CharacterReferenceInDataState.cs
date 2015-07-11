using System;
using System.Collections.Specialized;
using System.IO;
using System.Collections.Generic;

namespace WebEngineSharp.Tokenizer.States
{
    // 8.2.4.2 http://www.w3.org/TR/html5/syntax.html#character-reference-in-data-state
    public class CharacterReferenceInDataState : BaseState
    {
        private CharacterReferenceInDataState()
        {
        }

        private static CharacterReferenceInDataState s_Instance = new CharacterReferenceInDataState();

        public static CharacterReferenceInDataState Instance
        {
            get { return s_Instance; }
        }

        public override BaseState Process(HtmlTokenizer tokenizer, StreamReader reader)
        {
            return Process(tokenizer, reader, null);
        }

        public BaseState Process(HtmlTokenizer tokenizer, StreamReader reader, char? additionalAllowedCharacter)
        {
            // Switch to the data state.
            // Attempt to consume a character reference, with no additional allowed character.
            // (http://www.w3.org/TR/html5/syntax.html#tokenizing-character-references)
            // (http://www.w3.org/TR/html5/syntax.html#additional-allowed-character)
            //
            // If nothing is returned, emit a U+0026 AMPERSAND character (&) token.
            // Otherwise, emit the character tokens that were returned.

            int c = Peek(reader);

            if (c == 9 || c == 0x0A || c == 0x0C || c == ' ' || c == '<' || c == -1 ||
                additionalAllowedCharacter.HasValue && c == additionalAllowedCharacter.Value)
            {

            } else if (c == '#')
            {
                Read(reader);
                int nc = Peek(reader);
                uint? val = null;
                if (nc == 'X' || nc == 'x')
                {
                    Read(reader);
                    val = ConsumeHexDigits(reader);
                    // http://www.w3.org/TR/html5/infrastructure.html#ascii-hex-digits
                } else
                {
                    val = ConsumeDigits(reader);
                }

                if (val.HasValue)
                {
                    char parsedChar = GetCharFromNumericValue(val.Value);
                    tokenizer.EmitChar(parsedChar);
                } else
                {
                    tokenizer.EmitChar('&');
                }
            } else
            {
                // Consume the maximum number of characters possible, with the consumed characters matching one of the identifiers in the first column of the named character references table (in a case-sensitive manner).
                // If no match can be made, then no characters are consumed, and nothing is returned. In this case, if the characters after the U+0026 AMPERSAND character (&) consist of a sequence of one or more alphanumeric ASCII characters followed by a U+003B SEMICOLON character (;), then this is a parse error.
                // If the character reference is being consumed as part of an attribute, and the last character matched is not a ";" (U+003B) character, and the next character is either a "=" (U+003D) character or an alphanumeric ASCII character, then, for historical reasons, all the characters that were matched after the U+0026 AMPERSAND character (&) must be unconsumed, and nothing is returned. However, if this next character is in fact a "=" (U+003D) character, then this is a parse error, because some legacy user agents will misinterpret the markup in those cases.
                // Otherwise, a character reference is parsed. If the last character matched is not a ";" (U+003B) character, there is a parse error.
                // Return one or two character tokens for the character(s) corresponding to the character reference name (as given by the second column of the named character references table).
                //
                // Code Example:
                // If the markup contains (not in an attribute) the string "I'm &notit; I tell you", the character reference is
                // parsed as "not", as in, "I'm ¬it; I tell you" (and this is a parse error). But if the markup was
                // "I'm &notin; I tell you", the character reference would be parsed as "notin;", resulting in "I'm ∉ I tell you"
                // (and no parse error).

            }
            return DataState.Instance;
        }

        private static readonly HashSet<char> s_HexDigits = new HashSet<char>("0123456789ABCDEFabcdef");
        private static readonly HashSet<char> s_Digits = new HashSet<char>("0123456789");
        private static readonly Dictionary<uint, char> s_CharsFromValues = new Dictionary<uint, char>() {
            { 0x00, '\uFFFD' },
            { 0x80, '\u20AC' },
            { 0x82, '\u201A' },
            { 0x83, '\u0192' },
            { 0x84, '\u201E' },
            { 0x85, '\u2026' },
            { 0x86, '\u2020' },
            { 0x87, '\u2021' },
            { 0x88, '\u02C6' },
            { 0x89, '\u2030' },
            { 0x8A, '\u0160' },
            { 0x8B, '\u2039' },
            { 0x8C, '\u0152' },
            { 0x8E, '\u017D' },
            { 0x91, '\u2018' },
            { 0x92, '\u2019' },
            { 0x93, '\u201C' },
            { 0x94, '\u201D' },
            { 0x95, '\u2022' },
            { 0x96, '\u2013' },
            { 0x97, '\u2014' },
            { 0x98, '\u02DC' },
            { 0x99, '\u2122' },
            { 0x9A, '\u0161' },
            { 0x9B, '\u203A' },
            { 0x9C, '\u0153' },
            { 0x9E, '\u017E' },
            { 0x9F, '\u0178' }
        };
        private static readonly HashSet<uint> s_BadChars = new HashSet<uint>(
                                                               new uint[] {
                0x000B, 0xFFFE, 0xFFFF, 0x1FFFE, 0x1FFFF, 0x2FFFE, 0x2FFFF, 0x3FFFE, 0x3FFFF, 0x4FFFE, 0x4FFFF, 0x5FFFE,
                0x5FFFF, 0x6FFFE, 0x6FFFF, 0x7FFFE, 0x7FFFF, 0x8FFFE, 0x8FFFF, 0x9FFFE, 0x9FFFF, 0xAFFFE, 0xAFFFF, 0xBFFFE,
                0xBFFFF, 0xCFFFE, 0xCFFFF, 0xDFFFE, 0xDFFFF, 0xEFFFE, 0xEFFFF, 0xFFFFE, 0xFFFFF, 0x10FFFE, 0x10FFFF
            }
                                                           );

        private uint? ConsumeHexDigits(StreamReader reader)
        {
            string str = ConsumeCharacters(reader, s_HexDigits, ';');
            if (string.IsNullOrEmpty(str))
            {
                reader.BaseStream.Seek(-2, SeekOrigin.Current);
                ReportParseError();
                return null;
            }
            uint val = uint.Parse(str, System.Globalization.NumberStyles.HexNumber);
            return val;
        }

        private uint? ConsumeDigits(StreamReader reader)
        {
            string str = ConsumeCharacters(reader, s_Digits, ';');
            if (string.IsNullOrEmpty(str))
            {
                reader.BaseStream.Seek(-1, SeekOrigin.Current);
                ReportParseError();
                return null;
            }
            uint val = uint.Parse(str);
            return val;
        }

        private char GetCharFromNumericValue(uint val)
        {
            char c;
            if (s_CharsFromValues.TryGetValue(val, out c))
            {
                return c;
            }

            if ((val >= 0xD800 && val <= 0xDFFF) || (val > 0x10FFFF))
            {
                ReportParseError();
                return '\uFFFD';
            }

            if (s_BadChars.Contains(val))
            {
                ReportParseError();
            }
            return (char)val;
        }
    }
}
