using System;
using System.Collections.Specialized;
using System.IO;

namespace WebEngineSharp.Tokenizer.States
{
    // 8.2.4.56 http://www.w3.org/TR/html5/syntax.html#after-doctype-public-keyword-state
    public class AfterDocTypePublicKeywordState : BaseState
    {
        private AfterDocTypePublicKeywordState()
        {
        }

        private static AfterDocTypePublicKeywordState s_Instance = new AfterDocTypePublicKeywordState();

        public static AfterDocTypePublicKeywordState Instance
        {
            get { return s_Instance; }
        }

        public DocTypeToken Token { get; set; }

        public override BaseState Process(HtmlTokenizer tokenizer, StreamReader reader)
        {
            int c = Read(reader);
            if (IsWhitespace(c))
            {
                BeforeDocTypePublicIdentifierState.Instance.Token = Token;
                return BeforeDocTypePublicIdentifierState.Instance;
            }

            if (c == '"')
            {
                ReportParseError();
                Token.PublicIdentifier = string.Empty;
                DocTypePublicIdentifierQuotedState.InstanceDoubleQuoted.Token = Token;
                return DocTypePublicIdentifierQuotedState.InstanceDoubleQuoted;
            }

            if (c == '\'')
            {
                ReportParseError();
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
