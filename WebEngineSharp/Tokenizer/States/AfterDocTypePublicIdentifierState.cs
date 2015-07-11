using System;
using System.Collections.Specialized;
using System.IO;

namespace WebEngineSharp.Tokenizer.States
{
    // 8.2.4.60 http://www.w3.org/TR/html5/syntax.html#after-doctype-public-identifier-state
    public class AfterDocTypePublicIdentifierState : BaseState
    {
        private AfterDocTypePublicIdentifierState()
        {
        }

        private static AfterDocTypePublicIdentifierState s_Instance = new AfterDocTypePublicIdentifierState();

        public static AfterDocTypePublicIdentifierState Instance
        {
            get { return s_Instance; }
        }

        public DocTypeToken Token { get; set; }

        public override BaseState Process(HtmlTokenizer tokenizer, StreamReader reader)
        {
            int c = Read(reader);
            if (IsWhitespace(c))
            {
                BetweenDocTypePublicAndSystemIdentifiersState.Instance.Token = Token;
                return BetweenDocTypePublicAndSystemIdentifiersState.Instance;
            }

            if (c == '>')
            {
                tokenizer.EmitToken(Token);
                return DataState.Instance;
            }

            if (c == '"')
            {
                ReportParseError();
                Token.SystemIdentifier = string.Empty;
                DocTypeSystemIdentifierQuotedState.InstanceDoubleQuoted.Token = Token;
                return DocTypeSystemIdentifierQuotedState.InstanceDoubleQuoted;
            }

            if (c == '\'')
            {
                ReportParseError();
                Token.SystemIdentifier = string.Empty;
                DocTypeSystemIdentifierQuotedState.InstanceSingleQuoted.Token = Token;
                return DocTypeSystemIdentifierQuotedState.InstanceSingleQuoted;
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
            return BogusDocTypeState.Instance;
        }
    }
}
