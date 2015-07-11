using System;
using System.IO;

namespace WebEngineSharp.Tokenizer.States
{
    // 8.2.4.40 http://www.w3.org/TR/html5/syntax.html#attribute-value-(unquoted)-state
    public class AttributeValueUnquotedState: BaseState
    {
        private AttributeValueUnquotedState()
        {
        }

        private static AttributeValueUnquotedState s_Instance = new AttributeValueUnquotedState();

        public static AttributeValueUnquotedState Instance
        {
            get { return s_Instance; }
        }

        public AttributeToken Token { get; set; }

        public override BaseState Process(HtmlTokenizer tokenizer, StreamReader reader)
        {
            throw new NotImplementedException();
        }
	}

}

