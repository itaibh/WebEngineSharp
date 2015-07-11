using System;
using System.IO;

namespace WebEngineSharp.Tokenizer.States
{
    // 8.2.4.37 http://www.w3.org/TR/html5/syntax.html#before-attribute-value-state
    public class BeforeAttributeValueState : BaseState
    {
        private BeforeAttributeValueState()
        {
        }

        private static BeforeAttributeValueState s_Instance = new BeforeAttributeValueState();

        public static BeforeAttributeValueState Instance
        {
            get { return s_Instance; }
        }

        public AttributeToken Token { get; set; }

        public override BaseState Process(HtmlTokenizer tokenizer, StreamReader reader)
        {
            int c;
            do
            {
                c = Read(reader);
            } while (IsWhitespace(c));

            if (c == '"')
            {
                AttributeValueQuotedState.InstanceDoubleQuoted.Token = Token;
                return AttributeValueQuotedState.InstanceDoubleQuoted;
            }

            if (c == '&')
            {
                AttributeValueUnquotedState.Instance.Token = Token;
                AttributeValueUnquotedState.Instance.LastConsumedCharacters.Enqueue((char)c);
                return AttributeValueUnquotedState.Instance;
            }

            if (c == '\'')
            {
                AttributeValueQuotedState.InstanceSingleQuoted.Token = Token;
                return AttributeValueQuotedState.InstanceSingleQuoted;
            }

            if (c == 0)
            {
                ReportParseError();
                Token.AttributeValue += "\uFFFD";
                AttributeValueUnquotedState.Instance.Token = Token;
                return AttributeValueUnquotedState.Instance;
            }

            if (c == '>')
            {
                ReportParseError();
                tokenizer.EmitToken(Token);
                return DataState.Instance;
            }

            if (c == -1)
            {
                ReportParseError();
                return DataState.Instance; //reconsume... ?
            }

            if (c == '<' || c == '=' || c == '`')
            {
                ReportParseError();
            }

            Token.AttributeValue += (char)c;
            AttributeValueUnquotedState.Instance.Token = Token;
            return AttributeValueUnquotedState.Instance;
        }
    }
}

