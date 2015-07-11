using System;
using System.IO;

namespace WebEngineSharp.Tokenizer.States
{
    // 8.2.4.43 http://www.w3.org/TR/html5/syntax.html#self-closing-start-tag-state
    public class SelfClosingStartTagState : BaseState
    {
        private SelfClosingStartTagState()
        {
        }

        private static SelfClosingStartTagState s_Instance = new SelfClosingStartTagState();

        public static SelfClosingStartTagState Instance
        {
            get { return s_Instance; }
        }

        public TagToken Token { get; set; }

        public override BaseState Process(HtmlTokenizer tokenizer, StreamReader reader)
        {
            throw new NotImplementedException();
        }
    }

}

