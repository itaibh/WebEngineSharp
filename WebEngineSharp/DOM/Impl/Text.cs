using System;

namespace WebEngineSharp.DOM.Impl
{
    public class Text : CharacterData, IText
    {
        public Text(IDocument doc, string data)
            :base(doc, data)
        {
        }
    }
}

