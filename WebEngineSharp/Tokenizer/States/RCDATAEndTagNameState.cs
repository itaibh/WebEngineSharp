using System;
using System.IO;

namespace WebEngineSharp.Tokenizer.States
{
    //8.2.4.13 http://www.w3.org/TR/html5/syntax.html#rcdata-end-tag-name-state
    public class RCDATAEndTagNameState : BaseState
    {
        #region singleton

        private RCDATAEndTagNameState()
        {
        }

        private static RCDATAEndTagNameState s_Instance = new RCDATAEndTagNameState();

        public static RCDATAEndTagNameState Instance
        {
            get{ return s_Instance; }
        }

        #endregion

        public EndTagToken Token { get; set; }

        public override BaseState Process(HtmlTokenizer tokenizer, StreamReader reader)
        {
            int c = Read(reader);
            if (IsWhitespace(c))
            {
                if (tokenizer.IsAppropriateEndTagToken(Token))
                {
                    return BeforeAttributeNameState.Instance;
                }
            }
            else if (c == '/')
            {
                if (tokenizer.IsAppropriateEndTagToken(Token))
                {
                    return SelfClosingStartTagState.Instance;
                }
            }
            else if (c == '>')
            {
                if (tokenizer.IsAppropriateEndTagToken(Token))
                {
                    tokenizer.EmitToken(Token); // TODO - is this the right token to emit?
                    return DataState.Instance;
                }
            }
            else if (IsUppercaseAsciiLetter(c))
            {
                Token.TagName += Char.ToLower((char)c);
                tokenizer.TemporaryBuffer.Add((char)c);
                return this;
            }
            else if (IsLowercaseAsciiLetter(c))
            {
                Token.TagName += (char)c;
                tokenizer.TemporaryBuffer.Add((char)c);
                return this;
            }

            tokenizer.EmitChar('<');
            tokenizer.EmitChar('/');
            foreach (char bc in tokenizer.TemporaryBuffer)
            {
                tokenizer.EmitChar(bc);
            }
            RCDATAState.Instance.LastConsumedCharacters.Enqueue((char)c);
            return RCDATAState.Instance;
        }
    }
}
