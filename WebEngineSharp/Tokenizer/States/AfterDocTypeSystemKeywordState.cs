using System;
using System.Collections.Specialized;
using System.IO;

namespace WebEngineSharp.Tokenizer.States
{
    // 8.2.4.62 http://www.w3.org/TR/html5/syntax.html#after-doctype-system-keyword-state
    public class AfterDocTypeSystemKeywordState: BaseState
    {
        private AfterDocTypeSystemKeywordState()
        {
        }

        private static AfterDocTypeSystemKeywordState s_Instance = new AfterDocTypeSystemKeywordState();

        public static AfterDocTypeSystemKeywordState Instance
        {
            get { return s_Instance; }
        }

        public override BaseState Process(HtmlTokenizer tokenizer, StreamReader reader)
        {
            throw new NotImplementedException();
        }
	}


}
