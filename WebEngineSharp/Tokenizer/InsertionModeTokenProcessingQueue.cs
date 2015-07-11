using System;
using System.Collections.Generic;
using WebEngineSharp.Tokenizer.InsertionMode;
using WebEngineSharp.DOM;

namespace WebEngineSharp.Tokenizer
{
    public class InsertionModeTokenProcessingQueue : ITokenQueue
    {
        public InsertionModeTokenProcessingQueue(IDocument doc)
        {
            CurrentInsertionMode = InitialInsertionModeState.Instance;
            m_Document = doc;
        }

        private IDocument m_Document;

        private Queue<BaseToken> m_TokensQueue = new Queue<BaseToken>();
        private Queue<BaseToken> m_ReProcessTokensQueue = new Queue<BaseToken>();

        public BaseInsertionModeState CurrentInsertionMode { get; private set; }
        public BaseInsertionModeState OriginalInsertionMode { get; private set; }

        public void EnqueueToken(BaseToken token)
        {
            m_TokensQueue.Enqueue(token);
        }

        public void EnqueueTokenForReprocessing(BaseToken token)
        {
            m_ReProcessTokensQueue.Enqueue(token);
        }

        public void ProcessToken(HtmlTokenizer tokenizer)
        {
            if (CurrentInsertionMode == null)
            {
                return;
            }

            BaseToken token = null;
            if (m_ReProcessTokensQueue.Count > 0)
            {
                token = m_ReProcessTokensQueue.Dequeue();
            } else if (m_TokensQueue.Count > 0)
            {
                token = m_TokensQueue.Dequeue();
            }

            if (token != null)
            {
                CurrentInsertionMode = CurrentInsertionMode.ProcessToken(tokenizer, this, token, m_Document);
            }
        }

        public void SaveCurrentInsertionModeState()
        {
            OriginalInsertionMode = CurrentInsertionMode;
        }
    }
}

