using System;
using System.IO;

namespace WebEngineSharp.Tokenizer.States
{
    // 8.2.4.36 http://www.w3.org/TR/html5/syntax.html#after-attribute-name-state
    public class AfterAttributeNameState: BaseState
    {
        #region singleton
        private AfterAttributeNameState()
        {
        }

        private static AfterAttributeNameState s_Instance = new AfterAttributeNameState();

        public static AfterAttributeNameState Instance
        {
            get { return s_Instance; }
        }
        #endregion
        public AttributeToken Token { get; set; }

        public override BaseState Process(HtmlTokenizer tokenizer, StreamReader reader)
        {
            int c = Read(reader);
            throw new NotImplementedException();
        }
	}
}
