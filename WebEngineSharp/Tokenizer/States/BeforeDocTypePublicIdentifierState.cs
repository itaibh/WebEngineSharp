using System;
using System.Collections.Specialized;
using System.IO;

namespace WebEngineSharp.Tokenizer.States
{
    // 8.2.4.57 http://www.w3.org/TR/html5/syntax.html#before-doctype-public-identifier-state
    public class BeforeDocTypePublicIdentifierState : BaseState
    {
        private BeforeDocTypePublicIdentifierState()
        {
        }

        private static BeforeDocTypePublicIdentifierState s_Instance = new BeforeDocTypePublicIdentifierState();

        public static BeforeDocTypePublicIdentifierState Instance
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

            if (c == '"')
            {
                Token.PublicIdentifier = string.Empty;
                DocTypePublicIdentifierQuotedState.InstanceDoubleQuoted.Token = Token;
                return DocTypePublicIdentifierQuotedState.InstanceDoubleQuoted;
            }

            if (c == '\'')
            {
                Token.PublicIdentifier = string.Empty;
                DocTypePublicIdentifierQuotedState.InstanceSingleQuoted.Token = Token;
                return DocTypePublicIdentifierQuotedState.InstanceSingleQuoted;
            }

            if (c == '>')
            {
                ReportParseError();
                Token.ForceQuirks = true;
                tokenizer.EmitToken(Token);
                return DataState.Instance;
            }

            if (c == -1)
            {
                ReportParseError();
                Token.ForceQuirks = true;
                tokenizer.EmitToken(Token);
                return DataState.Instance;
                // Reconsume the EOF character. (?)
            }

            ReportParseError();
            Token.ForceQuirks = true;
            BogusDocTypeState.Instance.Token = Token;
            return BogusDocTypeState.Instance;
        }
	}

}
