using System;
using System.Collections.Specialized;
using System.IO;
using System.Collections.Generic;

namespace WebEngineSharp.Tokenizer.States
{
    // 8.2.4.53 http://www.w3.org/TR/html5/syntax.html#before-doctype-name-state
    public class BeforeDocTypeNameState : BaseState
    {
        private BeforeDocTypeNameState()
        {
        }

        private static BeforeDocTypeNameState s_Instance = new BeforeDocTypeNameState();

        public static BeforeDocTypeNameState Instance
        {
            get { return s_Instance; }
        }

        public override BaseState Process(HtmlTokenizer tokenizer, StreamReader reader)
        {
            int c;
            do
            {
                c = Read(reader);
            } while (IsWhitespace(c));

            DocTypeToken token = new DocTypeToken();
            DocTypeNameState.Instance.Token = token;

            if (base.IsUppercaseAsciiLetter(c))
            {
                token.Name = Char.ToLower((char)c).ToString();
                return DocTypeNameState.Instance;
            }

            if (c == 0)
            {
                ReportParseError();
                token.Name = "\uFFFD";
                return DocTypeNameState.Instance;
            }
            
            if (c == '>')
            {
                ReportParseError();
                token.ForceQuirks = true;
                tokenizer.EmitToken(token);
                return DataState.Instance;
            }

            if (c == -1)
            {
                ReportParseError();
                token.ForceQuirks = true;
                tokenizer.EmitToken(token);
                return DataState.Instance;
                // Parse error. Switch to the data state. Reconsume the EOF character.
            }

            token.Name = ((char)c).ToString();
            return DocTypeNameState.Instance;
        }
    }
}
