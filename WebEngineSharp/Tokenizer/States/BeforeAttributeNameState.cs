using System;
using System.IO;

namespace WebEngineSharp.Tokenizer.States
{
    // 8.2.4.34 http://www.w3.org/TR/html5/syntax.html#before-attribute-name-state
    public class BeforeAttributeNameState : BaseState
    {
        private BeforeAttributeNameState()
        {
        }

        private static BeforeAttributeNameState s_Instance = new BeforeAttributeNameState();

        public static BeforeAttributeNameState Instance
        {
            get { return s_Instance; }
        }

        public TagToken Token { get; set; }

        public override BaseState Process(HtmlTokenizer tokenizer, StreamReader reader)
        {
            AttributeToken attrToken = null;

            int c; 
            do
            {
                c = Read(reader);
            } while (IsWhitespace(c));

            if (c == '/')
            {
                return SelfClosingStartTagState.Instance;
            }

            if (c == '>')
            {
                tokenizer.EmitToken(Token);
                return DataState.Instance;
            }

            if (base.IsUppercaseAsciiLetter(c))
            {
                attrToken = new AttributeToken() {
                    AttributeName = Char.ToLower((char)c).ToString(),
                    AttributeValue = string.Empty,
                    ContainingTag = Token
                };
                Token.Attributes.Add(attrToken);
                AttributeNameState.Instance.Token = attrToken;
                return AttributeNameState.Instance;
            }

            if (c == 0)
            {
                ReportParseError();
                attrToken = new AttributeToken() {
                    AttributeName = "\uFFFD",
                    AttributeValue = string.Empty,
                    ContainingTag = Token
                };
                Token.Attributes.Add(attrToken);
                AttributeNameState.Instance.Token = attrToken;
                return AttributeNameState.Instance;
            }

            if (c == -1)
            {
                ReportParseError();
                return DataState.Instance;
                // Reconsume the EOF character (?)
            }

            if (c == '"' || c == '\'' || c == '<' || c == '=')
            {
                ReportParseError();
            } 
         
            attrToken = new AttributeToken() {
                AttributeName = ((char)c).ToString(),
                AttributeValue = string.Empty,
                ContainingTag = Token
            };

            Token.Attributes.Add(attrToken);
            AttributeNameState.Instance.Token = attrToken;
            return AttributeNameState.Instance;
        }
    }

}

