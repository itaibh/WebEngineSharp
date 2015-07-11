using System;
using System.IO;

namespace WebEngineSharp.Tokenizer.States
{
    // 8.2.4.38 http://www.w3.org/TR/html5/syntax.html#attribute-value-(double-quoted)-state
    // 8.2.4.39 http://www.w3.org/TR/html5/syntax.html#attribute-value-(single-quoted)-state
    public class AttributeValueQuotedState : BaseState
    {
        private char m_QuoteChar;

        private AttributeValueQuotedState(char quoteChar)
        {
            m_QuoteChar = quoteChar;
        }

        private static AttributeValueQuotedState s_InstanceSingleQuoted = new AttributeValueQuotedState('\'');
        private static AttributeValueQuotedState s_InstanceDoubleQuoted = new AttributeValueQuotedState('"');

        public static AttributeValueQuotedState InstanceSingleQuoted
        {
            get { return s_InstanceSingleQuoted; }
        }

        public static AttributeValueQuotedState InstanceDoubleQuoted
        {
            get { return s_InstanceDoubleQuoted; }
        }

        public AttributeToken Token { get; set; }

        public override BaseState Process(HtmlTokenizer tokenizer, StreamReader reader)
        {
            for (;;)
            {
                int c = Read(reader);
                if (c == m_QuoteChar)
                {
                    AfterAttributeValueQuotedState.Instance.Token = Token;
                    return AfterAttributeValueQuotedState.Instance;
                }

                if (c == '&')
                {
                    CharacterReferenceInAttributeValueState.Instance.Process(reader, m_QuoteChar, Token);
                    continue;
                }

                if (c == 0)
                {
                    ReportParseError();
                    Token.AttributeValue += '\uFFFD';
                    continue;
                } 

                if (c == -1)
                {
                    ReportParseError();
                    return DataState.Instance;
                    // Reconsume the EOF character. (?)
                }

                Token.AttributeValue += (char)c;
            }
        }
    }

}

