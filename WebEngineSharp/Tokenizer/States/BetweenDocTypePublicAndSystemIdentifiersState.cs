using System;
using System.Collections.Specialized;
using System.IO;

namespace WebEngineSharp.Tokenizer.States
{
    // 8.2.4.61 http://www.w3.org/TR/html5/syntax.html#between-doctype-public-and-system-identifiers-state
    public class BetweenDocTypePublicAndSystemIdentifiersState: BaseState
    {
        private BetweenDocTypePublicAndSystemIdentifiersState()
        {
        }

        private static BetweenDocTypePublicAndSystemIdentifiersState s_Instance = new BetweenDocTypePublicAndSystemIdentifiersState();

        public static BetweenDocTypePublicAndSystemIdentifiersState Instance
        {
            get { return s_Instance; }
        }

        public DocTypeToken Token { get; set; }

        public override BaseState Process(HtmlTokenizer tokenizer, StreamReader reader){
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

            if (c == '"')
            {
                Token.SystemIdentifier = string.Empty;
                DocTypeSystemIdentifierQuotedState.InstanceDoubleQuoted.Token = Token;
                return DocTypeSystemIdentifierQuotedState.InstanceDoubleQuoted;
            }

            if (c == '\'')
            {
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
