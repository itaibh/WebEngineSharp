using System;
using System.Collections.Specialized;
using System.IO;

namespace WebEngineSharp.Tokenizer.States
{
    // 8.2.4.68 http://www.w3.org/TR/html5/syntax.html#cdata-section-state
    public class CdataSectionState: BaseState
    {
        private CdataSectionState()
        {
        }

        private static CdataSectionState s_Instance = new CdataSectionState();

        public static CdataSectionState Instance
        {
            get { return s_Instance; }
        }

        public override BaseState Process(HtmlTokenizer tokenizer, StreamReader reader)
        {
            throw new NotImplementedException();
        }
    }

}
