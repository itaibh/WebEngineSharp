using System;
using System.Collections.Specialized;
using System.IO;

namespace WebEngineSharp.Tokenizer.States
{
    // 8.2.4.64 http://www.w3.org/TR/html5/syntax.html#doctype-system-identifier-(double-quoted)-state
    // 8.2.4.65 http://www.w3.org/TR/html5/syntax.html#doctype-system-identifier-(single-quoted)-state
    public class DocTypeSystemIdentifierQuotedState: BaseState
    {
        private char m_QuoteChar;

        private DocTypeSystemIdentifierQuotedState(char quoteChar)
        {
            m_QuoteChar = quoteChar;
        }

        private static DocTypeSystemIdentifierQuotedState s_InstanceSingleQuoted = new DocTypeSystemIdentifierQuotedState('\'');
        private static DocTypeSystemIdentifierQuotedState s_InstanceDoubleQuoted = new DocTypeSystemIdentifierQuotedState('"');

        public static DocTypeSystemIdentifierQuotedState InstanceSingleQuoted
        {
            get { return s_InstanceSingleQuoted; }
        }

        public static DocTypeSystemIdentifierQuotedState InstanceDoubleQuoted
        {
            get { return s_InstanceDoubleQuoted; }
        }

        public DocTypeToken Token { get; set; }

        public override BaseState Process(HtmlTokenizer tokenizer, StreamReader reader)
        {
            for (;;)
            {
                int c = Read(reader);
                if (c == m_QuoteChar)
                {
                    AfterDocTypeSystemIdentifierState.Instance.Token = Token;
                    return AfterDocTypeSystemIdentifierState.Instance;
                }

                if (c == 0)
                {
                    ReportParseError();
                    Token.SystemIdentifier += '\uFFFD';
                    continue;
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

                Token.SystemIdentifier += (char)c;
            }
        }
	}

}
