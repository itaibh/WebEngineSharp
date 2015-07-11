using System;
using System.IO;

namespace WebEngineSharp.Tokenizer.States
{
    // 8.2.4.41 http://www.w3.org/TR/html5/syntax.html#character-reference-in-attribute-value-state
	public class CharacterReferenceInAttributeValueState
	{
        private CharacterReferenceInAttributeValueState()
        {
        }

        private static CharacterReferenceInAttributeValueState s_Instance = new CharacterReferenceInAttributeValueState();

        public static CharacterReferenceInAttributeValueState Instance
        {
            get { return s_Instance; }
        }

        public void Process(StreamReader reader, char m_QuoteChar, AttributeToken token)
        {
            throw new NotImplementedException();
        }
	}


}

