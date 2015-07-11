using System;
using System.IO;
using System.Collections.Generic;

namespace WebEngineSharp.Tokenizer.States
{
    // 8.2.4.35 http://www.w3.org/TR/html5/syntax.html#attribute-name-state
    public class AttributeNameState: BaseState
    {
        private AttributeNameState()
        {
        }

        private static AttributeNameState s_Instance = new AttributeNameState();

        public static AttributeNameState Instance
        {
            get { return s_Instance; }
        }

        public AttributeToken Token { get; set; }

        public override BaseState Process(HtmlTokenizer tokenizer, StreamReader reader)
        {
            for (;;)
            {
                int c = Read(reader);

                if (IsWhitespace(c))
                {
                    if (ValidateAttribute())
                        AfterAttributeNameState.Instance.Token = Token;
                    return AfterAttributeNameState.Instance;
                }

                if (c == '/')
                {
                    if (ValidateAttribute())
                        SelfClosingStartTagState.Instance.Token = Token.ContainingTag;
                    return SelfClosingStartTagState.Instance;
                }

                if (c == '=')
                {
                    if (ValidateAttribute())
                        BeforeAttributeValueState.Instance.Token = Token;
                    return BeforeAttributeValueState.Instance;
                }

                if (c == '>')
                {
                    if (ValidateAttribute())
                        tokenizer.EmitToken(Token);
                    return DataState.Instance;
                }

                if (base.IsUppercaseAsciiLetter(c))
                {
                    Token.AttributeName += Char.ToLower((char)c);
                    continue;
                }

                if (c == 0)
                {
                    ReportParseError();
                    Token.AttributeName += "\uFFFD";
                }

                if (c == -1)
                {
                    ReportParseError();
                    ValidateAttribute();
                    return DataState.Instance; //reconsume... ?
                }

                if (c == '"' || c == '\'' || c == '<')
                {
                    ReportParseError();
                }

                Token.AttributeName += ((char)c);
            }
        }

        /*
When the user agent leaves the attribute name state (and before emitting the tag token, if appropriate),
the complete attribute's name must be compared to the other attributes on the same token;
if there is already an attribute on the token with the exact same name, then this is a parse error and the
new attribute must be removed from the token.

Note:
If an attribute is so removed from a token, it, along with the value that gets associated with it,
if any, are never subsequently used by the parser, and are therefore effectively discarded.
Removing the attribute in this way does not change its status as the "current attribute" for the purposes
of the tokenizer, however.


*/

        private bool ValidateAttribute()
        {
            if (Token == null)
                return false;

            bool removeAttribute = false;
            foreach (AttributeToken attr in Token.ContainingTag.Attributes)
            {
                if (Token != attr && Token.AttributeName == attr.AttributeName)
                {
                    ReportParseError();
                    removeAttribute = true;
                    break;
                }
            }

            if (removeAttribute)
            {
                Token.ContainingTag.Attributes.Remove(Token);
                Token.ContainingTag = null;
                Token = null;
                return false;
            }

            return true;
        }
    }


}

