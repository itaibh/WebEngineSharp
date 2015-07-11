using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using WebEngineSharp.DOM;
using WebEngineSharp.Tokenizer.States;

namespace WebEngineSharp.Tokenizer
{
    public class HtmlTokenizer
    {
        public HtmlTokenizer()
        {
            TemporaryBuffer = new List<char>();
            ScriptingEnabled = true;
        }

        private BaseState m_CurrentState = DataState.Instance;
        private bool m_StateChangedExternally = false;

        public List<char> TemporaryBuffer { get; private set; }

        public bool ParserPauseFlag { get; set; }

        public int ScriptNestingLevel { get; set; }

        public bool ScriptingEnabled { get; set; }

        internal void SetNextState(BaseState state)
        {
            m_CurrentState = state; 
            m_StateChangedExternally = true;
        }

        public IDocument Tokenize(Stream htmlStream)
        {
            StreamReader reader = new StreamReader(htmlStream);

            m_CurrentState = DataState.Instance;
            while (m_CurrentState != null)
            {
                m_StateChangedExternally = false;
                BaseState nextState = m_CurrentState.Process(this, reader);
                if (!m_StateChangedExternally)
                {
                    m_CurrentState = nextState;
                }
            }

            return TreeConstruction.Instance.Document;
        }

        public void EmitChar(char parsedChar)
        {
            TreeConstruction.Instance.ProcessToken(this, parsedChar);
        }

        public void EmitToken(BaseToken token)
        {
            StartTagToken startTagToken = token as StartTagToken;
            if (startTagToken != null)
            {
                m_LastEmittedStartTag = startTagToken;
            }
            TreeConstruction.Instance.ProcessToken(this, token);
        }

        private StartTagToken m_LastEmittedStartTag;

        public bool IsAppropriateEndTagToken(EndTagToken token)
        {
            if (m_LastEmittedStartTag == null || token == null)
            {
                return false;
            }

            return m_LastEmittedStartTag.TagName == token.TagName;
        }
    }
}
