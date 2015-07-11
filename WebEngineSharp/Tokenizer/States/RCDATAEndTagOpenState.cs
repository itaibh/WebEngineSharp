using System;
using System.Collections.Generic;
using System.IO;

namespace WebEngineSharp.Tokenizer.States
{
    //8.2.4.12 http://www.w3.org/TR/html5/syntax.html#rcdata-end-tag-open-state
    public class RCDATAEndTagOpenState : BaseState
    {
        #region singleton

        private RCDATAEndTagOpenState()
        {
        }

        private static RCDATAEndTagOpenState s_Instance = new RCDATAEndTagOpenState();

        public static RCDATAEndTagOpenState Instance
        {
            get{ return s_Instance; }
        }

        #endregion

        public override BaseState Process(HtmlTokenizer tokenizer, StreamReader reader)
        {
            int c = Read(reader);
            if (base.IsUppercaseAsciiLetter(c))
            {
                EndTagToken token = new EndTagToken(){ TagName = Char.ToLower((char)c).ToString() };
                //Create a new end tag token, and set its tag name to the lowercase version of the current input 
                //character (add 0x0020 to the character's code point).
                //Append the current input character to the temporary buffer. (http://www.w3.org/TR/html5/syntax.html#temporary-buffer)
                //Finally, switch to the RCDATA end tag name state. (Don't emit the token yet;
                //further details will be filled in before it is emitted.)
                tokenizer.TemporaryBuffer.Add((char)c);
                RCDATAEndTagNameState.Instance.Token = token;
                return RCDATAEndTagNameState.Instance;
            }
            if (base.IsLowercaseAsciiLetter(c))
            {
                EndTagToken token = new EndTagToken(){ TagName = ((char)c).ToString() };
                //Create a new end tag token, and set its tag name to the current input character.
                //Append the current input character to the temporary buffer. (http://www.w3.org/TR/html5/syntax.html#temporary-buffer)
                //Finally, switch to the RCDATA end tag name state. (Don't emit the token yet;
                //further details will be filled in before it is emitted.)
                tokenizer.TemporaryBuffer.Add((char)c);
                RCDATAEndTagNameState.Instance.Token = token;
                return RCDATAEndTagNameState.Instance;
            }
            tokenizer.EmitChar('<');
            tokenizer.EmitChar('/');
            LastConsumedCharacters.Enqueue((char)c);
            return RCDATAState.Instance;
        }
    }
}
