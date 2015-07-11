using System;
using System.IO;

namespace WebEngineSharp.Tokenizer.States
{
    // 8.2.4.42 http://www.w3.org/TR/html5/syntax.html#after-attribute-value-(quoted)-state
    public class AfterAttributeValueQuotedState: BaseState
    {
        private AfterAttributeValueQuotedState()
        {
        }

        private static AfterAttributeValueQuotedState s_Instance = new AfterAttributeValueQuotedState();

        public static AfterAttributeValueQuotedState Instance
        {
            get { return s_Instance; }
        }

        public AttributeToken Token { get; set; }

        public override BaseState Process(HtmlTokenizer tokenizer, StreamReader reader)
        {
            int c = Read(reader);
            if (IsWhitespace(c))
            {
                BeforeAttributeNameState.Instance.Token = Token.ContainingTag;
                return BeforeAttributeNameState.Instance;
            }

            if (c == '/')
            {
                SelfClosingStartTagState.Instance.Token = Token.ContainingTag;
                return SelfClosingStartTagState.Instance;
            }

            if (c == '>')
            {
                tokenizer.EmitToken(Token);
                return DataState.Instance;
            }

            if (c == -1)
            {
                ReportParseError();
                return DataState.Instance;
                //reconsume...
            }

            ReportParseError();
            BeforeAttributeNameState.Instance.Token = Token.ContainingTag;
            BeforeDocTypeNameState.Instance.LastConsumedCharacters.Enqueue((char)c);
            return BeforeAttributeNameState.Instance;
        }
    }


}

