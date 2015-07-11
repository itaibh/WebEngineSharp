using System;
using System.Collections.Specialized;
using System.IO;

namespace WebEngineSharp.Tokenizer.States
{
    // 8.2.4.55 http://www.w3.org/TR/html5/syntax.html#after-doctype-name-state
    public class AfterDocTypeNameState : BaseState
    {
        private AfterDocTypeNameState()
        {
        }

        private static AfterDocTypeNameState s_Instance = new AfterDocTypeNameState();

        public static AfterDocTypeNameState Instance
        {
            get { return s_Instance; }
        }

        public DocTypeToken Token { get; set; }

        public override BaseState Process(HtmlTokenizer tokenizer, StreamReader reader)
        {
            int c;
            do
            {
                c = Read(reader);
            } while (IsWhitespace(c));

            if (c == '>')
            {
                tokenizer.EmitToken(Token);
                return DataState.Instance;
            }

            if (c == -1)
            {
                // Parse error. Switch to the data state. Set the DOCTYPE token's force-quirks flag to on. Emit that DOCTYPE token. Reconsume the EOF character.
                ReportParseError();
                Token.ForceQuirks = true;
                tokenizer.EmitToken(Token);
                return DataState.Instance;
            }

            char[] buffer = new char[6];
            buffer[0] = (char)c;
            ReadBlock(reader, buffer, 1, 5);
            string bufferStr = new string(buffer);

            // If the six characters starting from the current input character are an ASCII case-insensitive match for the
            // word "PUBLIC", then consume those characters and switch to the after DOCTYPE public keyword state.
            if (bufferStr.Equals("public", StringComparison.OrdinalIgnoreCase))
            {
                AfterDocTypePublicKeywordState.Instance.Token = Token;
                return AfterDocTypePublicKeywordState.Instance;
            }

            // Otherwise, if the six characters starting from the current input character are an ASCII case-insensitive match
            // for the word "SYSTEM", then consume those characters and switch to the after DOCTYPE system keyword state.
            if (bufferStr.Equals("system", StringComparison.OrdinalIgnoreCase))
            {
                return AfterDocTypeSystemKeywordState.Instance;
            }

            // Otherwise, this is a parse error. Set the DOCTYPE token's force-quirks flag to on. Switch to the bogus DOCTYPE
            // state.
            reader.BaseStream.Seek(-6, SeekOrigin.Current);
            ReportParseError();
            Token.ForceQuirks = true;
            BogusCommentState.Instance.Comment.Append(bufferStr);
            return BogusCommentState.Instance;
        }
    }

}
