using System;
using System.Collections.Specialized;
using System.IO;

namespace WebEngineSharp.Tokenizer.States
{
    // 8.2.4.58 http://www.w3.org/TR/html5/syntax.html#doctype-public-identifier-(double-quoted)-state
    // 8.2.4.59 http://www.w3.org/TR/html5/syntax.html#doctype-public-identifier-(single-quoted)-state
    public class DocTypePublicIdentifierQuotedState : BaseState
    {
        private char m_QuoteChar;

        private DocTypePublicIdentifierQuotedState(char quoteChar)
        {
            m_QuoteChar = quoteChar;
        }

        private static DocTypePublicIdentifierQuotedState s_InstanceSingleQuoted = new DocTypePublicIdentifierQuotedState('\'');
        private static DocTypePublicIdentifierQuotedState s_InstanceDoubleQuoted = new DocTypePublicIdentifierQuotedState('"');

        public static DocTypePublicIdentifierQuotedState InstanceSingleQuoted
        {
            get { return s_InstanceSingleQuoted; }
        }

        public static DocTypePublicIdentifierQuotedState InstanceDoubleQuoted
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
                    AfterDocTypePublicIdentifierState.Instance.Token = Token;
                    return AfterDocTypePublicIdentifierState.Instance;
                }

                if (c == 0)
                {
                    ReportParseError();
                    Token.PublicIdentifier += '\uFFFD';
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

                Token.PublicIdentifier += (char)c;
            }
        }
    }
}
