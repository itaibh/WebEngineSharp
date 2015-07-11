using System;

namespace WebEngineSharp.DOM
{
    // http://www.w3.org/TR/dom/#processinginstruction
    public interface IProcessingInstruction : ICharacterData
    {
        string target { get; }
    }
}

