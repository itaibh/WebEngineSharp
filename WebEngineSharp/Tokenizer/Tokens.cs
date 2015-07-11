using System;
using System.Collections.Specialized;
using System.IO;

namespace WebEngineSharp.Tokenizer
{

    // http://www.w3.org/TR/html5/syntax.html#data-state

    // http://www.w3.org/TR/html5/syntax.html#character-reference-in-data-state

    // http://www.w3.org/TR/html5/syntax.html#tag-open-state

    public enum Tokens{
        DOCTYPE,
        StartTag,
        EndTag,
        Comment,
        Character,
        EndOfFile
    }
}
