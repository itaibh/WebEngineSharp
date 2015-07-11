using System;
using System.Collections.Generic;
using WebEngineSharp.DOM;

namespace WebEngineSharp.Tokenizer
{
    public class CharacterToken : BaseToken
	{
        public char Character { get; set; }

        private static readonly Dictionary<char, string> s_charStrings = new Dictionary<char, string>(){
            {(char)10,@"\n"}
        };
        public override string ToString()
        {
            string charStr;
            if (!s_charStrings.TryGetValue(Character, out charStr))
            {
                charStr = Character.ToString();
            }
            return string.Format("[CharacterToken: Character='{0}']", charStr);
        }
	}

}

