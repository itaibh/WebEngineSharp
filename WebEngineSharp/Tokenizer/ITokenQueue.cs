using System;
using System.Collections.Generic;
using WebEngineSharp.Tokenizer.InsertionMode;
using WebEngineSharp.DOM;

namespace WebEngineSharp.Tokenizer
{
	public interface ITokenQueue
	{
        void EnqueueToken(BaseToken token);
        void EnqueueTokenForReprocessing(BaseToken token);
	}

}

